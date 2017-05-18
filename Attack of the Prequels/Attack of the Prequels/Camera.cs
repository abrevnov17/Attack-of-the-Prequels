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
    class Camera
    {
        public Vector3 cameraPosition;
        public Vector3 cameraTarget;
        public Vector3 cameraUpVector;
        public float hAngle;
        public float vAngle;
        private Vector3 offset;
        

        public Camera()
        {
            
            cameraPosition = new Vector3(0, 0, 175);
            cameraTarget = new Vector3(0, 0, 0);
            cameraUpVector = Vector3.UnitY;
            hAngle = 0f;
            vAngle = 0f;
            offset = new Vector3(0f, 25, -300);


        }

        public void updateCamera(Player p,Matrix playerWorldMatrix)
        {
            //Player player = p;
           
            

        }

        public Matrix getViewMatrix(Matrix view,Player p)
        {
            
            //quaternion camera based on player position
            //if (p==Game1.player2)
            Matrix playerWorldMatrix = p.world;
            cameraPosition = playerWorldMatrix.Translation + (playerWorldMatrix.Up * 552f) + (playerWorldMatrix.Backward * 102f);
            cameraTarget = playerWorldMatrix.Translation;
            view = Matrix.CreateLookAt(cameraPosition, cameraTarget,playerWorldMatrix.Backward);
             return view;
        }
        public Matrix getViewMatrix(Matrix view, Player2 p)
        {

            //quaternion camera based on player position
            //if (p==Game1.player2)
            Matrix playerWorldMatrix = p.world;
            cameraPosition = playerWorldMatrix.Translation + (playerWorldMatrix.Up * 552f) + (playerWorldMatrix.Backward * 102f);
            cameraTarget = playerWorldMatrix.Translation;
            view = Matrix.CreateLookAt(cameraPosition, cameraTarget, playerWorldMatrix.Backward);
            return view;
        }
        public Matrix getProjection(Matrix projection,Player p)
        {


            return projection;
        }

    }
}
 