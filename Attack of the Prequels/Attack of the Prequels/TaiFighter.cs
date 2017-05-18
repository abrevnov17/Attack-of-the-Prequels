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
    class TaiFighter : EnemyShip
    {
        Player p;
        private int count = 1;
        ContentManager Content;
        int fireRate = Game1.rand.Next(70, 80);
        int count2;
        int count3 = 500;
        
        public TaiFighter(ContentManager Content,Player p,Random rand)
            : base(Content, p)
        {

            this.p = p;
            health = 1;
            velocity = p.initialVelocity - p.initialVelocity/2.5f;
            this.Content = Content;
            angleVelocity = 1f;
            count2=count3+1;
            


            position = new Vector3(Game1.deathStarPosition.X + rand.Next(0, 900000), Game1.deathStarPosition.Y + rand.Next(0, 900000), Game1.deathStarPosition.Z - rand.Next(0, 900000));
        }

        // the new forward vector, so the avatar faces the target



public static Quaternion GetRotation(Vector3 source, Vector3 dest, Vector3 up)
{
    float dot = Vector3.Dot(source, dest);

    if (Math.Abs(dot - (-1.0f)) < 0.000001f)
    {
        
        return new Quaternion(up, MathHelper.ToRadians(180.0f));
    }
   if (Math.Abs(dot - (1.0f)) < 0.000001f)
    {
      
        return Quaternion.Identity;
    }
    

    float rotAngle = (float)Math.Acos(dot);
    Vector3 rotAxis = Vector3.Cross(source, dest);
    rotAxis = Vector3.Normalize(rotAxis);
    return Quaternion.CreateFromAxisAngle(rotAxis, rotAngle);
}

        public override void move()
        {
            //change angles

            //takes durection of player

            Vector3 direction = world.Backward;
            direction.Normalize();
            
            /*
             
           this.position += direction * velocity;
           world = Matrix.CreateTranslation(position);
            
           */
            if (count % fireRate == 0)
            {
                float volume = 0f;
                volume = Vector3.Distance(p.position, position);
                
                
                    if (volume < 90000)
                    {
                        volume = 0.2f;
                    }
                    else if (volume < 150000)
                    {
                        volume = 0.1f;

                    }
                    else if (volume < 1900000)
                    {
                        volume = 0.01f;
                    }
                    else if (volume < 2000000)
                    {
                        volume = 0.001f;
                    }
                    else
                    {

                        volume = 0;
                    }
                
                float pitch = 0.0f;
                float pan = 0.0f;
                Game1.enemyLazerEffect.Play(volume, pitch, pan);
                //Console.WriteLine(Vector3.Distance(p.position,position));
               Game1.enemyLazers.Add(new Lazer(Content, p.world, p.velocity, direction, position));

            }
            count++;



            // Quaternion additionalRot = Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), (float)Math.Atan2(p.position.X - this.position.X, (double)0)) * Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), (float)Math.Atan2(0, p.position.Y - this.position.Y)); //* Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), angle);
            if (Vector3.Distance(p.position, position) > 172000 && count2>=count3)
            {
               // count = 0;
                Vector3 newForward = Vector3.Normalize(position - p.position);
                // calc the rotation so the avatar faces the target
                quaternion = Quaternion.Lerp(quaternion, GetRotation(Vector3.Forward, newForward, Vector3.Up), angleVelocity);
                //Cannon.Shoot(Position, Rotation, this);
                world = Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(quaternion) * Matrix.CreateTranslation(position);
                base.MoveForward(ref position, quaternion, velocity);
            }
            else

            {
                if (count2 >= count3)
                {
                    count2 = 0;
                    //Console.WriteLine("a");

                }
                count2++;



                Vector3 target = Vector3.Add(p.position, Vector3.Multiply(p.world.Backward,500000f));
                Vector3 newForward = Vector3.Normalize(position - target);
                // calc the rotation so the avatar faces the target
                quaternion = Quaternion.Lerp(quaternion, GetRotation(Vector3.Forward, newForward, Vector3.Up), angleVelocity/10f);
                //Cannon.Shoot(Position, Rotation, this);
                world = Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(quaternion) * Matrix.CreateTranslation(position);
                base.MoveForward(ref position, quaternion, velocity);

            }

            //Console.WriteLine(Vector3.Distance(position,p.position));
        }
    }
}