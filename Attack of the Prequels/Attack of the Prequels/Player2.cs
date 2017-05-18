﻿using System;
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
    class Player2 : EnemyShip
    {
        public float  rollAngle;
        public float initialVelocity = 1000;
        
        // public bool pNumber;


       // Model model;

      //  public Matrix world;

        public List<Lazer> lazers;
        ContentManager content;
       // public float scale;
         
        // ModelMesh modelMesh=model.mesh;
        // BoundingBox boundingBox=ModelMesh
        public Quaternion taiFighterRotation = Quaternion.Identity;
       
      
        protected float inverseFireRate;
        public bool inverted;
        private int lastShotCount;
        public int barrelRollCountY;
        public int barrelRollCountX;
        public PlayerIndex index;
        

        public Player2(ContentManager Content, Player p, PlayerIndex index)
            : base(Content, p)
        {

            angle = 0f;
            RotationMatrix = Matrix.Identity;
            //this.pNumber = pNumber;
            health = 10;
            scale = 100f;
            verticleAngle = 0f;
            velocity = 1000f;
            lastShotCount = 0;
            angleVelocity = 0.03f;
            position = new Vector3(0f, 0, 0f);
            model = Content.Load<Model>("xwingfinal");
            world = Matrix.CreateScale(scale) * Matrix.CreateTranslation(position);
            //world = Matrix.CreateScale(scale) * RotationMatrix * world;
            lazers = new List<Lazer>();
            this.content = Content;
            // initialVelocity = velocity;
            barrelRollCountY = 100;
            barrelRollCountX = 100;
            inverseFireRate = 9f;
            rollAngle = 0f;
            this.index = index;
            //this.view = view;
            //this.projection = projection;
            inverted = false;
        }

        // the new forward vector, so the avatar faces the target





       override public void update()
        {
            rollAngle = 0f;

#if XBOX360
            if (GamePad.GetState(index).Buttons.Y == ButtonState.Pressed&&barrelRollCountY>=65){

                barrelRollCountY=0;
            }


          

#endif
            if (barrelRollCountY < 65)
            {

                rollAngle = rollAngle - 0.05f;
                barrelRollCountY++;

            }

#if XBOX360
            if (GamePad.GetState(index).Buttons.A == ButtonState.Pressed&&barrelRollCountX>=65){

                barrelRollCountX=0;
            }


          

#endif
            if (barrelRollCountX < 65)
            {

                rollAngle = rollAngle + 0.05f;
                barrelRollCountX++;

            }
            //firelazercheck
            //GamePad.GetState(index).ThumbSticks.Left.

#if XBOX360
            if (GamePad.GetState(index).Triggers.Right>=0.01f)
#else
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.H))
#endif

            {
                if (lastShotCount % inverseFireRate == 0)
                {
                    Lazer lazer = new Lazer(content, world, initialVelocity+initialVelocity/2, world.Down, world.Translation + (world.Forward * 50f));
                    lazers.Add(lazer);
                    Game1.lazerEffect.Play();
                    lastShotCount = 1;
                }
                else
                {
                    lastShotCount++;

                }

            }


            
            
            angle = 0;
            verticleAngle = 0;
            //movementAngleStuff

#if XBOX360
               if (GamePad.GetState(index).DPad.Up == ButtonState.Pressed){
            if (inverted == false)
                {
                    verticleAngle = verticleAngle - angleVelocity/10;
                }
                else
                {
                    verticleAngle = verticleAngle + angleVelocity/10;
                }
            }
               if (GamePad.GetState(index).ThumbSticks.Right.Y >0f){
                
                 double x = 2;
                 double y = -13.0589;
                if (inverted == false)
                {
                 verticleAngle = verticleAngle - (float)Math.Pow(x, y + 8*GamePad.GetState(index).ThumbSticks.Right.Y);
            } 
            else {
                verticleAngle = verticleAngle + (float)Math.Pow(x, y + 8*GamePad.GetState(index).ThumbSticks.Right.Y);
            }
            }
#else
            if (state.IsKeyDown(Keys.I))
            {
                if (inverted == false)
                {
                    verticleAngle = verticleAngle - angleVelocity;
                }
                else
                {
                    verticleAngle = verticleAngle + angleVelocity;
                }
            }

#endif
#if XBOX360
             if (GamePad.GetState(index).Triggers.Left >=0.1f)
#else
            if (state.IsKeyDown(Keys.U))
#endif
            {
                if (velocity - initialVelocity < 2000)
                {
                    velocity = velocity + 75f;
                }
            }
            else
            {
                if (velocity > initialVelocity)
                {
                    velocity = velocity - 75f;

                }
            }

#if XBOX360
             if (GamePad.GetState(index).Buttons.LeftShoulder == ButtonState.Pressed)
#else
            if (state.IsKeyDown(Keys.O))
#endif
            {
                if (velocity - initialVelocity > -500)
                {
                    velocity = velocity - 75f;
                }
            }

            else
            {
                if (velocity < initialVelocity)
                {
                    velocity = velocity + 75f;

                }
            }

#if XBOX360
             if (GamePad.GetState(index).DPad.Down == ButtonState.Pressed){
                
            if (inverted == false)
                {
                    verticleAngle = verticleAngle + angleVelocity/10;
                }
                else
                {
                    verticleAngle = verticleAngle - angleVelocity/10;
                }
            

            }
            if (GamePad.GetState(index).ThumbSticks.Right.Y < 0f ){
                 double x = 2;
                 double y = -13.0589;
            if (inverted==false){
                 verticleAngle = verticleAngle + (float)Math.Pow(x, y - 8*GamePad.GetState(index).ThumbSticks.Right.Y);

            }

            else {
                verticleAngle = verticleAngle  - (float)Math.Pow(x, y -  8*GamePad.GetState(index).ThumbSticks.Right.Y);
            }

            }
#else
            if (state.IsKeyDown(Keys.K))
            {
                if (inverted == false)
                {
                    verticleAngle = verticleAngle + angleVelocity;
                }
                else
                {
                    verticleAngle = verticleAngle - angleVelocity;
                }

            }
#endif
#if XBOX360
            if (GamePad.GetState(index).ThumbSticks.Left.X >0.8f){
                rollAngle=rollAngle-angleVelocity;
            }
           else  if (GamePad.GetState(index).ThumbSticks.Left.X <-0.8f){
                rollAngle=rollAngle+angleVelocity;
            }
#endif

#if XBOX360
             if (GamePad.GetState(index).DPad.Left == ButtonState.Pressed){
                
            angle=angle+angleVelocity/10;
            
            }
            if (GamePad.GetState(index).ThumbSticks.Right.X <0f){
            double x = 2;
                double y = -13.0589;
                angle = angle + (float)Math.Pow(x, y - 8*GamePad.GetState(index).ThumbSticks.Right.X);
            }
#else
            if (state.IsKeyDown(Keys.J))
            {

                //Console.WriteLine(world.Translation);
                angle = angle + angleVelocity;
                //angle -= 0.03f;
            }
#endif
#if XBOX360
             if (GamePad.GetState(index).DPad.Right == ButtonState.Pressed){
            
                
            angle=angle-angleVelocity/10;
            
            

            }
            if (GamePad.GetState(index).ThumbSticks.Right.X >0f){
                double x = 2;
                 double y = -13.0589;
                angle = angle - (float)Math.Pow(x, y + 8*GamePad.GetState(index).ThumbSticks.Right.X);
            }
#else
            if (state.IsKeyDown(Keys.L))
            {
                angle = angle - angleVelocity;

            }
#endif
            Quaternion additionalRot = Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), rollAngle) * Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), angle) * Quaternion.CreateFromAxisAngle(new Vector3(1, 0, 0), verticleAngle); //* Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), angle);


            //angle = angle % (float)(2 * Math.PI);


            //position -= new Vector3(velocity * (float)Math.Sin(angle), -(velocity*(float)Math.Sin(verticleAngle)), velocity * (float)Math.Cos(angle));
            //quaternions
            taiFighterRotation *= additionalRot;
            //world = Matrix.CreateRotationY(angle) * Matrix.CreateTranslation(position);
            world = Matrix.CreateScale(scale) * Matrix.CreateFromQuaternion(taiFighterRotation) * Matrix.CreateTranslation(position);
            // position+=Vector3.Normalize(world.Forward)*velocity;
            MoveForward2(ref position, taiFighterRotation, velocity);





        }

         private void MoveForward2(ref Vector3 position, Quaternion rotationQuat, float speed)
        {
            Vector3 addVector = Vector3.Transform(new Vector3(0, -1, 0), rotationQuat);
            position += addVector * speed;
        }

        
    }
}