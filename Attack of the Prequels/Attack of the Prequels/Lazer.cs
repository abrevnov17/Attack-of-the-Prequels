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
    public class Lazer
    {
        public Vector3 lazerPosition;
        Model lazerModel;

        Vector3 direction;

        private Matrix lazerWorld;
        public float scale;
       public float velocity;

         public Lazer(ContentManager Content, Matrix playerWorldMatrix, float velocity, Vector3 direction, Vector3 lazerPosition)
        {

            this.lazerPosition = lazerPosition;
            lazerModel = Content.Load<Model>("lazer");
            lazerWorld = Matrix.CreateTranslation(lazerPosition);
            this.direction = direction;
            this.velocity = velocity + 2.5f*velocity;
            scale = 100f;

         }

         public void update()
         {
             //chase given direction
             lazerPosition += Vector3.Normalize(direction) * velocity;
             lazerWorld =Matrix.CreateScale(scale)*Matrix.CreateTranslation(lazerPosition);

             //checkCollisions

         }
        
         public void display()
         {
             DrawModel(lazerModel, lazerWorld, Game1.view, Game1.projection);

         }
        
         
         public void display(Matrix view)
         {
             DrawModel(lazerModel, lazerWorld, view, Game1.projection);

         }
        
         private void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
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
                     effect.AmbientLightColor = new Vector3(0.3f, 0.3f, 0.3f);

                 }

                 mesh.Draw();
             }
         }
    }
}
