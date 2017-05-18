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
    class EnemyBase
    {

        //simple enemy base
        Vector3 enemyPosition;
        Model enemyModel;

        private Matrix enemyWorld;
        protected float scale;
        
        public EnemyBase(ContentManager Content, String name, Vector3 position)
         {
            if (name.Equals("strikeBack8")){
             scale = 0.5f;

            }
            else {
                scale=200f;
            }
             enemyModel = Content.Load<Model>(name);
             this.enemyPosition = position;
             enemyWorld = Matrix.CreateScale(scale)*Matrix.CreateTranslation(position);


         }

        public void update()
        {

            //doesnothing

        }
        public void display()
        {

            DrawModel(enemyModel, enemyWorld, Game1.view, Game1.projection);

        }
        virtual public void display(Matrix view)
        {

            DrawModel(enemyModel, enemyWorld, view, Game1.projection);

        }
        /*
        private static BoundingBox CreateBoundingBox(Model model)
        {
            Matrix[] boneTransforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(boneTransforms);

            BoundingBox result = new BoundingBox();
            foreach (ModelMesh mesh in model.Meshes)
                foreach (ModelMeshPart meshPart in mesh.MeshParts)
                {
                    BoundingBox? meshPartBoundingBox = GetBoundingBox(meshPart, boneTransforms[mesh.ParentBone.Index]);
                    if (meshPartBoundingBox != null)
                        result = BoundingBox.CreateMerged(result, meshPartBoundingBox.Value);
                }
            return result;
        }
         */
        

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
                    effect.AmbientLightColor = new Vector3(0.1f, 0.1f,0.1f);

                    

                }

                mesh.Draw();
            }
        }

    }
}
