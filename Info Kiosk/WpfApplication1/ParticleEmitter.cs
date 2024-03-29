﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;

namespace WpfApplication1
{
    public struct ParticleVertexFormat
    {
        public Vector2 initialPosition;
        public Vector2 initialvelocity;
        public Vector2 acceleration;
        public HalfVector2 textureCoord;
        public Single timeCreated; //in milliseconds
        public Single timeToLive; //in milliseconds

        public ParticleVertexFormat(Vector2 initialPosition, Vector2 initialvelocity, Vector2 acceleration, HalfVector2 textureCoord, int timeCreated, int timeToLive)
        {
            this.initialPosition = initialPosition;
            this.initialvelocity = initialvelocity;
            this.acceleration = acceleration;
            this.textureCoord = textureCoord;
            this.timeCreated = timeCreated;
            this.timeToLive = timeToLive;
        }

        public readonly static VertexDeclaration VertexDeclaration = new VertexDeclaration
                (
                    new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),                             //initialposition
                    new VertexElement(sizeof(float) * 2, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),    //initialvelocity
                    new VertexElement(sizeof(float) * 4, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 1),    //acceleration
                    new VertexElement(sizeof(float) * 6, VertexElementFormat.HalfVector2, VertexElementUsage.TextureCoordinate, 2),//textureCoord
                    new VertexElement(sizeof(float) * 7, VertexElementFormat.Single, VertexElementUsage.TextureCoordinate, 3),     //time created
                    new VertexElement(sizeof(float) * 8, VertexElementFormat.Single, VertexElementUsage.TextureCoordinate, 4)      //time to live
                );
    }

    public class ParticleEmitter
    {
        #region Private Variables

        private int maxNumOfParticles;                      //max number of particles (set in constructor)
        private ParticleVertexFormat[] particleVertices;    //list of all particles' vertices
        private int[] particleIndices;                      //list of all particles' indices
        private DynamicVertexBuffer vertexBuffer;           //vertex buffer stored on gpu
        private IndexBuffer indexBuffer;                    //index buffer stored on gpu
        private int[] expirationTime;                       //expiration time for each particle in milliseconds
        private int numOfActiveParticles;                   //number of active particles
        private const int sizeOfParticle = 36;              //size of particle in bytes

        //Set by property
        private string m_effectTechnique = "FadeAtXPercent";
        private float m_fadeStartPercent = .80f;
        private float m_changePicPercent = .80f;

        //World/View/Projection Matrices
        private Matrix wMatrix;
        private Matrix viewMatrix;
        private Matrix projectionMatrix;

        //Effect file for shaders
        private Effect effect;

        //Texture for particle
        private Texture2D texture1;
        private Texture2D texture2;

        #endregion Private Variables

        #region Public Properties

        internal BlendState blendState = BlendState.Additive;
        internal string effectTechnique
        {
            get
            {
                return m_effectTechnique;
            }
            set
            {
                if (value == "FadeAtXPercent")
                    m_effectTechnique = value;
                else if (value == "ChangePicAndFadeAtPercent")
                    m_effectTechnique = value;
                else
                    m_effectTechnique = "FadeAtXPercent";
            }
        }
        internal float fadeStartPercent
        {
            get
            {
                return m_fadeStartPercent;
            }
            set
            {
                if (value < 0)
                    m_fadeStartPercent = 0.0f;
                else if (value > 1)
                    m_fadeStartPercent = 1.0f;
                else
                    m_fadeStartPercent = value;
                effect.Parameters["xFadeStartPercent"].SetValue(m_fadeStartPercent);
            }
        }
        internal float changePicPercent
        {
            get
            {
                return m_changePicPercent;
            }
            set
            {
                if (value < 0.0f)
                    m_changePicPercent = 0.0f;
                else if (value > 1.0f)
                    m_changePicPercent = 1.0f;
                else
                    m_changePicPercent = value;
                effect.Parameters["xChangePicPercent"].SetValue(m_changePicPercent);
            }
        }

        #endregion Public Properties

        public ParticleEmitter(int maxNumOfParticles, Effect effect, Texture2D texture1, Texture2D texture2 = null)
        {
            //sets max number of particles 
            this.maxNumOfParticles = maxNumOfParticles;

            //instantiate array of particle vertices & indicies
            particleVertices = new ParticleVertexFormat[maxNumOfParticles * 4];
            particleIndices = new int[maxNumOfParticles * 6];

            //instantiate array of time to lives to all particles
            expirationTime = new int[maxNumOfParticles];

            numOfActiveParticles = 0;

            //Allocate memory on gpu for vertices & indicies
            vertexBuffer = new DynamicVertexBuffer(MainWindow.GraphicsDevice, ParticleVertexFormat.VertexDeclaration, particleVertices.Length, BufferUsage.WriteOnly);
            indexBuffer = new IndexBuffer(MainWindow.GraphicsDevice, typeof(int), particleIndices.Length, BufferUsage.WriteOnly);

            //create all indices (they never change)
            for (int i = 0; i < maxNumOfParticles; i++)
                setIndices(i);

            //give buffer's their data
            vertexBuffer.SetData(particleVertices);
            indexBuffer.SetData(particleIndices);

            //load Texture
            this.texture1 = texture1;
            this.texture2 = texture2;

            //load the HLSL code
            this.effect = effect.Clone();

            //Create View and Projection Matrices
            wMatrix = Matrix.Identity;
            viewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, -10), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            projectionMatrix = Matrix.CreateOrthographic(MainWindow.GraphicsDevice.Viewport.Width, MainWindow.GraphicsDevice.Viewport.Height, -50f, 50f);

            sendConstantsToGPU();
        }

        public void update()
        {
            removeExpiredParticles();
        }

        public void sendConstantsToGPU()
        {
            effect.Parameters["xWorldViewProjection"].SetValue(wMatrix * viewMatrix * projectionMatrix);
            effect.Parameters["xWorld"].SetValue(wMatrix);
            effect.Parameters["xTexture1"].SetValue(texture1);
            effect.Parameters["xTexture2"].SetValue(texture2);
        }

        private void removeExpiredParticles()
        {
            int currentTime = (int)GlobalVariables.TotalTime.TotalMilliseconds;

            for (int i = 0; i < numOfActiveParticles; i++)
            {
                if (expirationTime[i] <= currentTime)
                {
                    deleteParticle(i);
                }
            }
        }

        public void createParticles(Vector2 velocity, Vector2 acceleration, Vector2 startPosition, float size, int timeToLive)
        {
            if (numOfActiveParticles < maxNumOfParticles)
            {
                //create particle vertices
                particleVertices[numOfActiveParticles * 4] = new ParticleVertexFormat(new Vector2(startPosition.X - size, startPosition.Y - size), velocity, acceleration, new HalfVector2(0, 0), (int)GlobalVariables.TotalTime.TotalMilliseconds, timeToLive);
                particleVertices[numOfActiveParticles * 4 + 1] = new ParticleVertexFormat(new Vector2(startPosition.X + size, startPosition.Y - size), velocity, acceleration, new HalfVector2(1, 0), (int)GlobalVariables.TotalTime.TotalMilliseconds, timeToLive);
                particleVertices[numOfActiveParticles * 4 + 2] = new ParticleVertexFormat(new Vector2(startPosition.X + size, startPosition.Y + size), velocity, acceleration, new HalfVector2(1, 1), (int)GlobalVariables.TotalTime.TotalMilliseconds, timeToLive);
                particleVertices[numOfActiveParticles * 4 + 3] = new ParticleVertexFormat(new Vector2(startPosition.X - size, startPosition.Y + size), velocity, acceleration, new HalfVector2(0, 1), (int)GlobalVariables.TotalTime.TotalMilliseconds, timeToLive);

                //set time to live
                expirationTime[numOfActiveParticles] = timeToLive + (int)GlobalVariables.TotalTime.TotalMilliseconds;

                moveParticleToGPU(numOfActiveParticles);

                //increment number of particles
                numOfActiveParticles++;
            }
        }

        //Move last active particle to new location
        private void deleteParticle(int particleNumber)
        {
            int lastParticle = numOfActiveParticles - 1;
            particleVertices[particleNumber * 4] = particleVertices[lastParticle * 4];
            particleVertices[particleNumber * 4 + 1] = particleVertices[lastParticle * 4 + 1];
            particleVertices[particleNumber * 4 + 2] = particleVertices[lastParticle * 4 + 2];
            particleVertices[particleNumber * 4 + 3] = particleVertices[lastParticle * 4 + 3];

            expirationTime[particleNumber] = expirationTime[lastParticle];

            moveParticleToGPU(particleNumber);

            numOfActiveParticles--;
        }

        private void setIndices(int particleNumber)
        {
            //set particle indices
            particleIndices[particleNumber * 6] = particleNumber * 4;
            particleIndices[particleNumber * 6 + 1] = particleNumber * 4 + 1;
            particleIndices[particleNumber * 6 + 2] = particleNumber * 4 + 2;
            particleIndices[particleNumber * 6 + 3] = particleNumber * 4 + 2;
            particleIndices[particleNumber * 6 + 4] = particleNumber * 4 + 3;
            particleIndices[particleNumber * 6 + 5] = particleNumber * 4;
        }

        private void moveParticleToGPU(int particleNumber)
        {
            MainWindow.GraphicsDevice.SetVertexBuffer(null);

            //overwrite vertex data on gpu
            vertexBuffer.SetData(sizeOfParticle * particleNumber * 4,       //start location on gpu buffer to overwrite
                                 particleVertices,                          //array to pull data from to overwrite gpu buffer
                                 particleNumber * 4,                        //first element in array to use
                                 4,                                         //number of elements in array to use
                                 sizeOfParticle,                            //size in bytes of one element in array
                                 SetDataOptions.None);                      //tell setData() to overwrite data
        }

        public void draw()
        {
            // Restore the vertex buffer contents if the graphics device was lost.
            if (vertexBuffer.IsContentLost)
                vertexBuffer.SetData(particleVertices);

            //Set buffers on gpu
            MainWindow.GraphicsDevice.SetVertexBuffer(vertexBuffer);
            MainWindow.GraphicsDevice.Indices = indexBuffer;

            //Set blendstate
            MainWindow.GraphicsDevice.BlendState = blendState;

            //Set the effect technique
            effect.CurrentTechnique = effect.Techniques[m_effectTechnique];

            //These lines store data in variables on the graphics card
            effect.Parameters["xCurrentTime"].SetValue((int)GlobalVariables.TotalTime.TotalMilliseconds);

            if (numOfActiveParticles > 0)
                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                {
                    pass.Apply();

                    MainWindow.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList,
                                                                  0,
                                                                  0,
                                                                  numOfActiveParticles * 4,
                                                                  0,
                                                                  numOfActiveParticles * 2
                                                                  );
                }
        }
    }
}
