using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N64PPLEditorC.TransparentPanel
{
    class DrawScene : PanelDraw
    {
        private List<Bitmap> bmpList { get; set; }
        private List<Point> posXY { get; set; }

        public void AddBmp(Bitmap bmp,Point posXY)
        {
            bmp.MakeTransparent(Color.FromArgb(0, 255, 0));
            this.bmpList.Add(bmp);
            this.posXY.Add(posXY);
            
            
        }
        public void Init()
        {
            this.bmpList = new List<Bitmap>();
            this.posXY = new List<Point>();
        }

        protected override void OnDraw()
        {
            this.graphics.Clear(Color.Transparent);
            if (bmpList != null)
            {
                for (int i = 0; i < bmpList.Count; i++)
                {
                    this.graphics.DrawImage(bmpList[i], posXY[i]);
                }
            }
        }
    }
}
