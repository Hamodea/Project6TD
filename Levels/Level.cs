using CatmullRom;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project6TD;
using Project6TD.Levels;

public class Level
{
    public Texture2D Background { get; private set; }
    public CatmullRomPath RoadPath { get; private set; }

    private RenderTarget2D placementMask;
    private Texture2D pixel;
    private GraphicsDevice graphicsDevice;

    private int roadWidth = 16;   // samma som DrawFillSetup
    private int roadMaskRadius = 8; // halva vägbredden ungefär

    private Vector2 startPos;
    private Vector2 endPos;
    private Vector2 castlePosition;
    public Level(GraphicsDevice graphicsDevice, ContentManager content)
    {
        this.graphicsDevice = graphicsDevice;

        Background = content.Load<Texture2D>("grassbg");

        pixel = new Texture2D(graphicsDevice, 1, 1);
        pixel.SetData(new[] { Color.White });

        CreatePath();
        startPos = RoadPath.EvaluateAt(0.02f) + new Vector2(0, -110);
        endPos = RoadPath.EvaluateAt(1f);
        // BERÄKNA RIKTNING
        Vector2 p1 = RoadPath.EvaluateAt(0.92f);
        Vector2 p2 = endPos;

        Vector2 direction = Vector2.Normalize(p2 - p1);

        //  FLYTTA SLOTTET FRAMÅT LÄNGS VÄGEN
        Vector2 basePosition = p2 + direction * 41f;
        Vector2 verticalOffset = new Vector2(0, -30);
        castlePosition = basePosition + verticalOffset;
        RoadPath.DrawFillSetup(graphicsDevice, (uint)roadWidth, 6, 30);

        CreatePlacementMask();
    }

    private void CreatePath()
    {
        RoadPath = new CatmullRomPath(graphicsDevice, 0.5f);
        RoadPath.Clear();
        LoadPath.LoadPathFromFile(RoadPath, "level1.txt");
    }

    private void CreatePlacementMask()
    {
        var vp = graphicsDevice.Viewport;

        placementMask = new RenderTarget2D(
            graphicsDevice,
            vp.Width,
            vp.Height
        );

        graphicsDevice.SetRenderTarget(placementMask);
        graphicsDevice.Clear(Color.White);

        SpriteBatch sb = new SpriteBatch(graphicsDevice);

        sb.Begin(SpriteSortMode.Deferred, BlendState.Opaque);

        for (float t = 0; t < 1f; t += 0.001f)
        {
            Vector2 pos = RoadPath.EvaluateAt(t);

            sb.Draw(pixel,
                new Rectangle(
                    (int)pos.X - roadMaskRadius,
                    (int)pos.Y - roadMaskRadius,
                    roadMaskRadius * 2,
                    roadMaskRadius * 2),
                Color.DarkRed);
        }

        sb.End();

        graphicsDevice.SetRenderTarget(null);
    }

    //public bool IsPlacementAllowed(int x, int y)
    //{
    //    if (placementMask == null)
    //        return false;

    //    if (x < 0 || x >= placementMask.Width ||
    //        y < 0 || y >= placementMask.Height)
    //        return false;

    //    Color[] data = new Color[1];
    //    placementMask.GetData(0, new Rectangle(x, y, 1, 1), data, 0, 1);

    //    var c = data[0];

    //    System.Diagnostics.Debug.WriteLine($"Pixel: {c}");

    //    return c.R > 200 && c.G > 200 && c.B > 200;
    //}
    public bool IsPlacementAllowed(int x, int y, int towerRadius)
    {
        if (placementMask == null)
            return false;

        for (int offsetX = -towerRadius; offsetX <= towerRadius; offsetX += 4)
        {
            for (int offsetY = -towerRadius; offsetY <= towerRadius; offsetY += 4)
            {
                int checkX = x + offsetX;
                int checkY = y + offsetY;

                if (checkX < 0 || checkX >= placementMask.Width ||
                    checkY < 0 || checkY >= placementMask.Height)
                    return false;

                Color[] data = new Color[1];
                placementMask.GetData(0, new Rectangle(checkX, checkY, 1, 1), data, 0, 1);

                Color c = data[0];

                if (!(c.R > 200 && c.G > 200 && c.B > 200))
                    return false;
            }
        }

        return true;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // Ingen scaling
        spriteBatch.Draw(Background, Vector2.Zero, Color.White);

        //Start pos
        
        spriteBatch.Draw(
               AssetsManager.startPosTex,
               startPos,
               null,
               Color.White,
               0f,
               new Vector2(AssetsManager.startPosTex.Width / 2f, AssetsManager.startPosTex.Height / 2f),
               1f,
               SpriteEffects.None,
               0f
        );

        spriteBatch.Draw(
            AssetsManager.castelTex,
            castlePosition,
            null,
            Color.White,
            0f,
            new Vector2(
           AssetsManager.castelTex.Width / 2f,
           AssetsManager.castelTex.Height / 2f),
            1.5f,
            SpriteEffects.None,
            0f
   );
    }

    public void DrawRoad()
    {
        RoadPath.DrawFill(graphicsDevice, AssetsManager.roadTex);
    }

    public void DrawDebugMask(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(placementMask, Vector2.Zero, Color.Red * 0.4f);
    }
}