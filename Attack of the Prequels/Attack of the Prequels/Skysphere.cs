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
    class Skysphere
    {

        Vector3 position;
        Model model;

        private Matrix sphereWorld;
        protected float scale;
        
        public Skysphere(ContentManager Content)
         {
             scale = 3095000f;
             model = Content.Load<Model>("final5");
             position = new Vector3(0f, 0f, -1500f);
             sphereWorld = Matrix.CreateScale(scale)*Matrix.CreateTranslation(new Vector3(0, 0, -1500f));


         }

        public void update()
        {

            //doesnothing

        }
        public void display()
        {

            DrawModel(model, sphereWorld, Game1.view, Game1.projection);

        }
        virtual public void display(Matrix view)
        {

            DrawModel(model, sphereWorld, view, Game1.projection);

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
                    effect.AmbientLightColor = new Vector3(0.5f, 0.5f, 0.5f);

                }

                mesh.Draw();
            }
        }
    }
}
