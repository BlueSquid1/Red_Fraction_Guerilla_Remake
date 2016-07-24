using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BulletSharp;
using OpenTK;
using System.Runtime.InteropServices;

namespace RFGR
{
    class Physics
    {
        static private BulletSharp.CollisionConfiguration collisionConf;
        static private BulletSharp.DynamicsWorld world;
        static private BulletSharp.Dispatcher dispatcher;
        static private BulletSharp.BroadphaseInterface broadphase;

        //list with all bodies
        static private List<BulletSharp.RigidBody> bodies = new List<BulletSharp.RigidBody> { };

        //constructor
        public Physics()
        {
            collisionConf = new BulletSharp.DefaultCollisionConfiguration();
            dispatcher = new BulletSharp.CollisionDispatcher(collisionConf);

            broadphase = new BulletSharp.DbvtBroadphase();
            world = new BulletSharp.DiscreteDynamicsWorld(dispatcher, broadphase, null, collisionConf);
            world.Gravity = new BulletSharp.Math.Vector3(0, -10, 0);
        }

        public BulletSharp.RigidBody AddPlane()
        {
            //place plane at 0,0,0 with angles along global axis
            BulletSharp.Math.Matrix transform = BulletSharp.Math.Matrix.Identity;
            //set shape of ground (infident in all dirrections)
            //face y dirrection
            float distFromOrigin = -1.0f;
            BulletSharp.StaticPlaneShape plane = new BulletSharp.StaticPlaneShape(new BulletSharp.Math.Vector3(0, 1, 0), distFromOrigin);
            //assign motion state to plane (for setting the location)
            BulletSharp.MotionState motion = new BulletSharp.DefaultMotionState(transform);


            //set physic properties
            float planeMass = 0.0f; //zero means its a static object
            BulletSharp.RigidBodyConstructionInfo info = new BulletSharp.RigidBodyConstructionInfo(planeMass, motion, plane);

            //create the body
            BulletSharp.RigidBody planeBody = new BulletSharp.RigidBody(info);
            //add it to the world
            world.AddRigidBody(planeBody);
            //add it to a list so I can access all its info
            bodies.Add(planeBody);

            return planeBody;
        }

        public BulletSharp.RigidBody AddSphere(float rad, BulletSharp.Math.Matrix startTransform, float mass)
        {

            //rigidbody is dynamic if and only if mass is non zero, otherwise static
            bool isDynamic = mass != 0.0f;
            BulletSharp.SphereShape sphere = new BulletSharp.SphereShape(rad);
            BulletSharp.DefaultMotionState motionState = new BulletSharp.DefaultMotionState(startTransform);
            //motionState.StartWorldTrans = startTransform;

            BulletSharp.Math.Vector3 inertial = new BulletSharp.Math.Vector3(0, 0, 0);

            if (isDynamic)
            {
                sphere.CalculateLocalInertia(mass, out inertial);
            }

            BulletSharp.RigidBodyConstructionInfo info = new BulletSharp.RigidBodyConstructionInfo(mass, motionState, sphere, inertial);
            BulletSharp.RigidBody body = new BulletSharp.RigidBody(info);
            info.Dispose();
            world.AddRigidBody(body);
            bodies.Add(body);

            return body;
        }

        public virtual void Update(float elapsedTime)
        {
            world.StepSimulation(elapsedTime);
        }

    }
}
