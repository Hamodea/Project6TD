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

    GraphicsDevice graphicsDevice;

    public Level(GraphicsDevice graphicsDevice, ContentManager content)
    {
        this.graphicsDevice = graphicsDevice;

        Background = content.Load<Texture2D>("grassbg");

        pixel = new Texture2D(graphicsDevice, 1, 1);
        pixel.SetData(new[] { Color.White });

        CreatePath();
        RoadPath.DrawFillSetup(graphicsDevice, 40, 6, 30);
        CreatePlacementMask();

    }

    private void CreatePath()
    {
        RoadPath = new CatmullRomPath(graphicsDevice, 0.35f);
        RoadPath.Clear();
        LoadPath.LoadPathFromFile(RoadPath, "level1.txt");
       
    }

    private void CreatePlacementMask()
    {
        placementMask = new RenderTarget2D(
            graphicsDevice,
            Background.Width,
            Background.Height
        );

        graphicsDevice.SetRenderTarget(placementMask);
        graphicsDevice.Clear(Color.White);

        SpriteBatch sb = new SpriteBatch(graphicsDevice);
        sb.Begin();

        var vp = graphicsDevice.Viewport;

        for (float t = 0; t < 1f; t += 0.005f)
        {
            Vector2 worldPos = RoadPath.EvaluateAt(t);

            // KONVERTERA WORLD → TEXTURE SPACE
            float u = worldPos.X / vp.Width;
            float v = worldPos.Y / vp.Height;

            int tx = (int)(u * placementMask.Width);
            int ty = (int)(v * placementMask.Height);

            int roadMaskRadius = 20;
            sb.Draw(pixel,
                new Rectangle(tx - roadMaskRadius, ty - roadMaskRadius, roadMaskRadius * 2, roadMaskRadius * 2),
                Color.Black);
        }

        sb.End();
        graphicsDevice.SetRenderTarget(null);
    }


    public bool IsPlacementAllowed(int x, int y)
    {
        if (placementMask == null)
            return false;

        // Mouse coords (x,y) are in window / backbuffer space.
        // placementMask is in background-texture space. Map window -> mask coordinates.
        var vp = graphicsDevice.Viewport;
        if (vp.Width == 0 || vp.Height == 0)
            return false;

        float u = (float)x / vp.Width;
        float v = (float)y / vp.Height;

        int tx = (int)(u * placementMask.Width);
        int ty = (int)(v * placementMask.Height);

        if (tx < 0 || tx >= placementMask.Width ||
            ty < 0 || ty >= placementMask.Height)
            return false;

        Color[] pixel = new Color[1];
        placementMask.GetData(0, new Rectangle(tx, ty, 1, 1), pixel, 0, 1);
        return pixel[0] == Color.White;
    }

    public void Draw(SpriteBatch spriteBatch)
    {


        var viewPort = graphicsDevice.Viewport;
        var dest = new Rectangle(0, 0, viewPort.Width, viewPort.Height);
        spriteBatch.Draw(Background, dest, Color.White);

    }

    public void DrawRoad(SpriteBatch spriteBatch)
    {
        RoadPath.DrawFill(graphicsDevice, AssetsManager.roadTex);
    }
    //public void DrawDebugMask(SpriteBatch spriteBatch)
    //{
    //    var vp = graphicsDevice.Viewport;
    //    var dest = new Rectangle(0, 0, vp.Width, vp.Height);

    //    spriteBatch.Draw(placementMask, dest, Color.Red * 0.3f);
    //}





}
