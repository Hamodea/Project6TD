using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MonoGame.ImGuiNet;
using System;
using System.Diagnostics;
namespace Project6TD
{
    public static class AssetsManager
    {
       
        public static Texture2D roadTex;
        public static Texture2D enemyTex;
        public static Texture2D towerTex;
        public static Texture2D pixel;
        public static Texture2D projectileTex;
        public static Texture2D enemy2Tex;
        public static Texture2D bakgroundTex;
        public static Animation EnemyWalk;
        public static Animation Enemy2Walk;
        public static Texture2D strongtowerTex;
        public static Texture2D cirkelTex;
        public static SoundEffect towerShoot;
        public static SoundEffect enemyDamage;
        public static Song gameIntro;
        public static Texture2D slowTowerTex;

       

        public static void LoadTexture(ContentManager content, GraphicsDevice graphicsDevice)
        {
            roadTex = content.Load<Texture2D>("Assets/Road3");
            enemyTex = content.Load<Texture2D>("Assets/enemysheet1");
            towerTex = content.Load<Texture2D>("Assets/tower1");
            projectileTex = content.Load<Texture2D>("Assets/projectile");
            enemy2Tex = content.Load<Texture2D>("Assets/walkSheet2");
            strongtowerTex = content.Load<Texture2D>("Assets/tower2");
            bakgroundTex = content.Load<Texture2D>("TDbg");
            cirkelTex = content.Load<Texture2D>("Assets/CircleTex");
            slowTowerTex = content.Load<Texture2D>("Assets/Tower3");


            




            pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });

            CreateEnemyAnimations();
            CreatNewEnemyAnmiation();
        }

        public static void LoadSounds(ContentManager content)
        {
            towerShoot = content.Load<SoundEffect>("Assets/Sounds/towerShoot");
            Debug.WriteLine("sound loaded");
            enemyDamage = content.Load<SoundEffect>("Assets/Sounds/enemyDamage");
            SoundEffect.MasterVolume = 1.0f;
            gameIntro = content.Load<Song>("Assets/Sounds/gameIntro"); 

        }

        private static void CreateEnemyAnimations()
        {
            int frameWidth = 55;
            int frameHeight = 82;
            int walkRow = 1;
            int frameCount = 8;
            int padding = 2;

            Rectangle[] walkFrames = new Rectangle[frameCount];

            for (int i = 0; i < frameCount; i++)
            {
                walkFrames[i] = new Rectangle(
                    i * (frameWidth + padding),
                    walkRow * frameHeight,
                    frameWidth,
                    frameHeight
                );
            }

            
            EnemyWalk = new Animation(enemyTex, walkFrames, 0.12f);
        }

        private static void CreatNewEnemyAnmiation()
        {
            int frameWidth = 85;
            int frameHeight = 150;
            int walkRow = 0;
            int frameCount = 4;
            int padding = 2;

            Rectangle[] walkFrame = new Rectangle[frameCount];
            for(int i = 0;i < frameCount; i++)
            {
                walkFrame[i] = new Rectangle(
                    i * (frameWidth + padding),
                    walkRow * frameHeight,
                    frameWidth,
                    frameHeight
                    );

            }
            Enemy2Walk = new Animation(enemy2Tex, walkFrame, 0.12f);
        }

    }
}
