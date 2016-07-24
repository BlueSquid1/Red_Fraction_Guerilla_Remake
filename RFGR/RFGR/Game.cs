using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing; //only used for color
using OpenTK.Input; //for keyboard input

namespace RFGR
{
    class Game : GameWindow
    {
        private Physics physicsEngine;



        //constructor
        public Game()
            : base(800, 600, new GraphicsMode(), "RFG Remake")
        {
            base.VSync = OpenTK.VSyncMode.Adaptive;
            physicsEngine = new Physics();
        }

        public bool LoadResources()
        {
            //add the ground
            physicsEngine.AddPlane();

            //add a sphere
            BulletSharp.Math.Matrix transformSphere = BulletSharp.Math.Matrix.Identity;
            transformSphere.M42 = 20;
            physicsEngine.AddSphere(1.0f, transformSphere, 1.0f);
            

            return true;
        }

        /*
        protected override void OnLoad(System.EventArgs e)
        {
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(System.Drawing.Color.MidnightBlue);

            GL.Enable(EnableCap.ColorMaterial);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.Lighting);
        }

        
        protected override void OnUnload(System.EventArgs e)
        {
            physicsEngine.ExitPhysics();
            base.OnUnload(e);
        }
        */

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            physicsEngine.Update((float)e.Time);


            KeyboardState state = OpenTK.Input.Keyboard.GetState();
            if (state.IsKeyDown(Key.Escape) || state.IsKeyDown(Key.Q))
            {
                Exit();
            }
        }

        /*
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            frameTime += (float)e.Time;
            fps++;
            if (frameTime >= 1)
            {
                frameTime = 0;
                Title = "BulletSharp OpenTK Demo, FPS = " + fps.ToString();
                fps = 0;
            }

            GL.Viewport(0, 0, Width, Height);

            float aspect_ratio = Width / (float)Height;
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 0.1f, 100);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);

            Matrix4 lookat = Matrix4.LookAt(new Vector3(10, 20, 30), Vector3.Zero, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);

            GL.Rotate(angle, 0.0f, 1.0f, 0.0f);
            angle += (float)e.Time * 100;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            InitCube();

            foreach (RigidBody body in physics.World.CollisionObjectArray)
            {
                Matrix4 modelLookAt = body.MotionState.WorldTransform * lookat;
                GL.LoadMatrix(ref modelLookAt);

                if ("Ground".Equals(body.UserObject))
                {
                    DrawCube(Color.Green, 50.0f);
                    continue;
                }

                if (body.ActivationState == ActivationState.ActiveTag)
                    DrawCube2(Color.Orange);
                else
                    DrawCube2(Color.Red);
            }

            UninitCube();

            SwapBuffers();
        }

                private void DrawCube(Color color, float size)
        {
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(color);
            GL.Vertex3(-size, -size, -size);
            GL.Vertex3(-size, size, -size);
            GL.Vertex3(size, size, -size);
            GL.Vertex3(size, -size, -size);

            GL.Vertex3(-size, -size, -size);
            GL.Vertex3(size, -size, -size);
            GL.Vertex3(size, -size, size);
            GL.Vertex3(-size, -size, size);

            GL.Vertex3(-size, -size, -size);
            GL.Vertex3(-size, -size, size);
            GL.Vertex3(-size, size, size);
            GL.Vertex3(-size, size, -size);
            
            GL.Vertex3(-size, -size, size);
            GL.Vertex3(size, -size, size);
            GL.Vertex3(size, size, size);
            GL.Vertex3(-size, size, size);

            GL.Vertex3(-size, size, -size);
            GL.Vertex3(-size, size, size);
            GL.Vertex3(size, size, size);
            GL.Vertex3(size, size, -size);

            GL.Vertex3(size, -size, -size);
            GL.Vertex3(size, size, -size);
            GL.Vertex3(size, size, size);
            GL.Vertex3(size, -size, size);

            GL.End();
        }
        */


    }
}
