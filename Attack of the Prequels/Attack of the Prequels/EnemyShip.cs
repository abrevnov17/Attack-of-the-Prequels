using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Attack_of_the_Prequels
{
     public class EnemyShip
    {
        //baseclass for enemy ship
        public float angle, verticleAngle, velocity, angleVelocity;
        public Vector3 position;
         public int lockOnCount;

        public Model model;

        public Matrix world;
        //public List<Lazer> lazers;
        ContentManager content;
        public float scale = 132f;
        public Matrix RotationMatrix = Matrix.Identity;
         public int health;
       

        public Quaternion quaternion = Quaternion.Identity;
        Player p;

        public EnemyShip(ContentManager Content, Player p)
        {
            angle = 0f;
            health = 1;
            //scale = 0.005f;
            verticleAngle = 0f;
            lockOnCount = 0;
            velocity = 5f;
            angleVelocity = 0.05f;
            
            position = new Vector3(0f, 0f, 0f);
            model = Content.Load<Model>("finalFighter");
            world = Matrix.CreateScale(scale) * Matrix.CreateTranslation(new Vector3(0, 0, 0));
            this.p = p;
            //world = Matrix.CreateScale(scale) * RotationMatrix * world;
           // lazers = new List<Lazer>();
            this.content = Content;

        }

        virtual public void update()
        {
            //shooting

            //movement


            this.move();

        }

        virtual public void move()
        {

            

            //angle = angle % (float)(2 * Math.PI);


           //position -= new Vector3(velocity * (float)Math.Sin(angle), -(velocity*(float)Math.Sin(verticleAngle)), velocity * (float)Math.Cos(angle));
           //quaternions

            //here I get the direction vector in between where I am pointing and where my enemy is located at
           Vector3 midVector = Vector3.Normalize(Vector3.Lerp(Vector3.Normalize(world.Forward), Vector3.Normalize(Vector3.Subtract(p.position,this.position)), angleVelocity));
            //here I get the vector perpendicular to this middle vector and my forward vector
           Vector3 perp=Vector3.Normalize(Vector3.Cross(midVector, Vector3.Normalize(world.Forward)));
            
            //here I am looking at my quaternion and I am trying to rotate it about the axis (my perp vector) with an angle that I determine which is in between where I am facing and the midVector
           float angle=(float)Math.Acos(Vector3.Dot(world.Forward,midVector));
           
           
           // Console.WriteLine(midVector+","+perp+","+angle);

           

           
         
               Quaternion quaternion2 = Quaternion.CreateFromAxisAngle(perp, angle);


               quaternion = quaternion * quaternion2;
           
            //here I am simply scaling my world matrix, implementing my quaternion, and translating it to my position
           world = Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(quaternion) * Matrix.CreateTranslation(position);

           
           //here i move myself forward in the direciton that I am facing
           MoveForward(ref position, quaternion, velocity);
            

        }

        virtual public void MoveForward(ref Vector3 position, Quaternion rotationQuat, float speed)
        {

            Vector3 addVector = Vector3.Transform(new Vector3(0, 0, -1), rotationQuat);
            position -= addVector * speed;
        }
        
    
        
        
        virtual public void display(Matrix view)
        {

            DrawModel(model, world, view, Game1.projection);

        }

        virtual public void display()
        {

            DrawModel(model, world, Game1.view, Game1.projection);

        }


        virtual public void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                    effect.EnableDefaultLighting();
                    effect.LightingEnabled = true;
                    effect.AmbientLightColor = new Vector3(0.7f, 0.7f, 0.7f);

                }

                mesh.Draw();
            }
        }
    }
}
