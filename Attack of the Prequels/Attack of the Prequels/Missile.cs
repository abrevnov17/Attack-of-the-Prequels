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
    public class Missile
    {
        public Vector3 lazerPosition;
        Model lazerModel;

        public EnemyShip target;

        private Matrix lazerWorld;
        public float scale;
        public float velocity;

        public Missile(ContentManager Content, Matrix playerWorldMatrix, float velocity, EnemyShip enemy, Vector3 lazerPosition)
        {

            this.lazerPosition = lazerPosition;
            lazerModel = Content.Load<Model>("missileFinal");
            lazerWorld = Matrix.CreateTranslation(lazerPosition);
            this.target = enemy;
            this.velocity = velocity + (float)1.2*velocity;
            scale = 232f;

        }

        public void update()
        {
            //chase given direction
            lazerPosition += Vector3.Normalize(target.position-lazerPosition) * velocity;
            lazerWorld = Matrix.CreateScale(scale) * Matrix.CreateTranslation(lazerPosition);

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
