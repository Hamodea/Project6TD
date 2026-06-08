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

    private int roadWidth = 40;   // samma som DrawFillSetup
    private int roadMaskRadius = 22; // halva vägbredden ungefär

    public Level(GraphicsDevice graphicsDevice, ContentManager content)
    {
        this.graphicsDevice = graphicsDevice;

        Background = content.Load<Texture2D>("grassbg");

        pixel = new Texture2D(graphicsDevice, 1, 1);
        pixel.SetData(new[] { Color.White });

        CreatePath();
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

        for (float t = 0; t < 1f; t += 0.002f)
        {
            Vector2 pos = RoadPath.EvaluateAt(t);

            sb.Draw(pixel,
                new Rectangle(
                    (int)pos.X - 22,
                    (int)pos.Y - 22,
                    44,
                    44),
                Color.Black);
        }

        sb.End();

        graphicsDevice.SetRenderTarget(null);
    }

    public bool IsPlacementAllowed(int x, int y)
    {
        if (placementMask == null)
            return false;

        if (x < 0 || x >= placementMask.Width ||
            y < 0 || y >= placementMask.Height)
            return false;

        Color[] data = new Color[1];
        placementMask.GetData(0, new Rectangle(x, y, 1, 1), data, 0, 1);

        var c = data[0];

        System.Diagnostics.Debug.WriteLine($"Pixel: {c}");

        return c.R > 200 && c.G > 200 && c.B > 200;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        // 🔥 Ingen scaling
        spriteBatch.Draw(Background, Vector2.Zero, Color.White);
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