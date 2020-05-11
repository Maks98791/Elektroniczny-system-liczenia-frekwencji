using AForge.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace AForge.Vision.Motion
{
    class CountingObjectsProcessing : IMotionProcessing
    {
        // blob counter to detect separate blobs
        public BlobCounter blobCounter = new BlobCounter();
        // color used for blobs highlighting
        private Color highlightColor = Color.Green;
        // highlight motion regions or not
        private bool highlightMotionRegions = true;
        private int count = 0;
        private int LR = 0;
        private int RL = 0; 

        Blob[] listLastFrame = null;
        Blob[] listThisFrame = null;

        /// <summary>
        /// Highlight motion regions or not.
        /// </summary>
        /// 
        /// <remarks><para>The property specifies if detected moving objects should be highlighted
        /// with rectangle or not.</para>
        /// 
        /// <para>Default value is set to <see langword="true"/>.</para>
        ///
        /// <para><note>Turning the value on leads to additional processing time of video frame.</note></para>
        /// </remarks>
        /// 
        public int getPersonCount()
        {
            return count; 
        }
        public int getLR()
        {
            return LR; 
        }
        public int getRL()
        {
            return RL;
        }

        public bool HighlightMotionRegions
        {
            get { return highlightMotionRegions; }
            set { highlightMotionRegions = value; }
        }

        /// <summary>
        /// Color used to highlight motion regions.
        /// </summary>
        /// 
        /// <remarks>
        /// <para>Default value is set to <b>red</b> color.</para>
        /// </remarks>
        /// 
        public Color HighlightColor
        {
            get { return highlightColor; }
            set { highlightColor = value; }
        }

        /// <summary>
        /// Minimum width of acceptable object.
        /// </summary>
        /// 
        /// <remarks><para>The property sets minimum width of an object to count and highlight. If
        /// objects have smaller width, they are not counted and are not highlighted.</para>
        /// 
        /// <para>Default value is set to <b>10</b>.</para>
        /// </remarks>
        /// 
        public int MinObjectsWidth
        {
            get { return blobCounter.MinWidth; }
            set
            {
                lock (blobCounter)
                {
                    blobCounter.MinWidth = value;
                }
            }
        }

        /// <summary>
        /// Minimum height of acceptable object.
        /// </summary>
        /// 
        /// <remarks><para>The property sets minimum height of an object to count and highlight. If
        /// objects have smaller height, they are not counted and are not highlighted.</para>
        /// 
        /// <para>Default value is set to <b>10</b>.</para>
        /// </remarks>
        /// 
        public int MinObjectsHeight
        {
            get { return blobCounter.MinHeight; }
            set
            {
                lock (blobCounter)
                {
                    blobCounter.MinHeight = value;
                }
            }
        }

        /// <summary>
        /// Number of detected objects.
        /// </summary>
        /// 
        /// <remarks><para>The property provides number of moving objects detected by
        /// the last call of <see cref="ProcessFrame"/> method.</para></remarks>
        /// 
        public int ObjectsCount
        {
            get
            {
                lock (blobCounter)
                {
                    return blobCounter.ObjectsCount;
                }
            }
        }

        /// <summary>
        /// Rectangles of moving objects.
        /// </summary>
        /// 
        /// <remarks><para>The property provides array of moving objects' rectangles
        /// detected by the last call of <see cref="ProcessFrame"/> method.</para></remarks>
        /// 
        public Rectangle[] ObjectRectangles
        {
            get
            {
                lock (blobCounter)
                {
                    return blobCounter.GetObjectsRectangles();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobCountingObjectsProcessing"/> class.
        /// </summary>
        /// 
        public CountingObjectsProcessing() : this( 10, 10 ) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobCountingObjectsProcessing"/> class.
        /// </summary>
        /// 
        /// <param name="highlightMotionRegions">Highlight motion regions or not (see <see cref="HighlightMotionRegions"/> property).</param>
        /// 
        public CountingObjectsProcessing(bool highlightMotionRegions) : this( 10, 10, highlightMotionRegions ) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobCountingObjectsProcessing"/> class.
        /// </summary>
        /// 
        /// <param name="minWidth">Minimum width of acceptable object (see <see cref="MinObjectsWidth"/> property).</param>
        /// <param name="minHeight">Minimum height of acceptable object (see <see cref="MinObjectsHeight"/> property).</param>
        /// 
        public CountingObjectsProcessing(int minWidth, int minHeight) :
            this( minWidth, minHeight, Color.Red ) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobCountingObjectsProcessing"/> class.
        /// </summary>
        /// 
        /// <param name="minWidth">Minimum width of acceptable object (see <see cref="MinObjectsWidth"/> property).</param>
        /// <param name="minHeight">Minimum height of acceptable object (see <see cref="MinObjectsHeight"/> property).</param>
        /// <param name="highlightColor">Color used to highlight motion regions.</param>
        /// 
        public CountingObjectsProcessing(int minWidth, int minHeight, Color highlightColor)
        {
            blobCounter.FilterBlobs = true;
            blobCounter.MinHeight = minHeight;
            blobCounter.MinWidth = minWidth;

            this.highlightColor = highlightColor;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlobCountingObjectsProcessing"/> class.
        /// </summary>
        /// 
        /// <param name="minWidth">Minimum width of acceptable object (see <see cref="MinObjectsWidth"/> property).</param>
        /// <param name="minHeight">Minimum height of acceptable object (see <see cref="MinObjectsHeight"/> property).</param>
        /// <param name="highlightMotionRegions">Highlight motion regions or not (see <see cref="HighlightMotionRegions"/> property).</param>
        /// 
        public CountingObjectsProcessing(int minWidth, int minHeight, bool highlightMotionRegions)
            : this( minWidth, minHeight )
        {
            this.highlightMotionRegions = highlightMotionRegions;
        }

        /// <summary>
        /// Process video and motion frames doing further post processing after
        /// performed motion detection.
        /// </summary>
        /// 
        /// <param name="videoFrame">Original video frame.</param>
        /// <param name="motionFrame">Motion frame provided by motion detection
        /// algorithm (see <see cref="IMotionDetector"/>).</param>
        /// 
        /// <remarks><para>Processes provided motion frame and counts number of separate
        /// objects, which size satisfies <see cref="MinObjectsWidth"/> and <see cref="MinObjectsHeight"/>
        /// properties. In the case if <see cref="HighlightMotionRegions"/> property is
        /// set to <see langword="true"/>, the found object are also highlighted on the
        /// original video frame.
        /// </para></remarks>
        /// 
        /// <exception cref="InvalidImagePropertiesException">Motion frame is not 8 bpp image, but it must be so.</exception>
        /// <exception cref="UnsupportedImageFormatException">Video frame must be 8 bpp grayscale image or 24/32 bpp color image.</exception>
        /// 
        public void ProcessFrame(UnmanagedImage videoFrame, UnmanagedImage motionFrame)
        {
            if (motionFrame.PixelFormat != PixelFormat.Format8bppIndexed)
            {
                throw new InvalidImagePropertiesException("Motion frame must be 8 bpp image.");
            }

            if ((videoFrame.PixelFormat != PixelFormat.Format8bppIndexed) &&
                 (videoFrame.PixelFormat != PixelFormat.Format24bppRgb) &&
                 (videoFrame.PixelFormat != PixelFormat.Format32bppRgb) &&
                 (videoFrame.PixelFormat != PixelFormat.Format32bppArgb))
            {
                throw new UnsupportedImageFormatException("Video frame must be 8 bpp grayscale image or 24/32 bpp color image.");
            }

            int width = videoFrame.Width;
            int height = videoFrame.Height;

            if ((motionFrame.Width != width) || (motionFrame.Height != height))
                return;

            lock (blobCounter)
            {
                blobCounter.ProcessImage(motionFrame);
            }
            Drawing.Line(videoFrame, new IntPoint(width/2, 0), new IntPoint(width/2, height), Color.Blue);
            if (highlightMotionRegions)
            {
                // highlight each moving object
                Rectangle[] rects = blobCounter.GetObjectsRectangles();

                for(int i = 0; i < rects.Length; i++ )
                {
                    if (rects[i].Left > width / 2)
                    {
                        Drawing.Rectangle(videoFrame, rects[i], highlightColor);
                    }
                    else
                        Drawing.Rectangle(videoFrame, rects[i], Color.Green);
                }
            }
            if (listLastFrame != null)
            {
                Blob find; 
                listThisFrame = blobCounter.GetObjectsInformation();
                foreach (Blob b in listThisFrame)
                {
                    find = findSimilarBlob(listLastFrame, b);
                    if (find != null)
                    {
                        if (find.Rectangle.Left <= width / 2 && b.Rectangle.Left > width / 2)
                        {
                            Console.WriteLine("Osoba przeszła w lewo");
                            RL++;
                            count = count + 1;

                        }
                        if (find.Rectangle.Right >= width / 2 && b.Rectangle.Right < width / 2)
                        {
                            Console.WriteLine("Osoba przeszła w prawo");
                            LR++; 
                            count = count - 1;
                        }
                    }
                }
            }
            listLastFrame = blobCounter.GetObjectsInformation();
        }

        private Blob findSimilarBlob(Blob[] list, Blob blob)
        {
            double best = Double.MaxValue;
            double result;
            int x, y; 
            Blob similar = null; 
            if (list.Length != 0)
            {
                similar = list[0];
                foreach (Blob b in list)
                {
                    x = System.Math.Abs(b.Rectangle.Location.X - blob.Rectangle.X);
                    y = System.Math.Abs(b.Rectangle.Location.Y - blob.Rectangle.Y);
                    result = System.Math.Sqrt((System.Math.Pow(x, 2) + System.Math.Pow(y, 2)));
                    if(result<best && result<20)
                    {
                        best = result;
                        similar = b;
                    }
                }
                return similar;
            }
            return similar; 
        }
        /// <summary>
        /// Reset internal state of motion processing algorithm.
        /// </summary>
        /// 
        /// <remarks><para>The method allows to reset internal state of motion processing
        /// algorithm and prepare it for processing of next video stream or to restart
        /// the algorithm.</para></remarks>
        ///
        public void Reset()
        {
        }
        
    }
}
