﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Limelight
{
    using Microsoft.Phone.Media;
    using System;
    using System.Diagnostics;
    using System.Windows;

        /// <summary>
        /// Renders video from the stream
        /// </summary>
        internal class VideoRenderer
        {
            // Indicates if rendering is already in progress or not
            private bool isRendering;
            private VideoStreamSource mediaStreamSource;
            private MediaStreamer mediaStreamer; 
            /// <summary>
            /// Constructor
            /// </summary>
            internal VideoRenderer()
            {
            }

            /// <summary>
            /// Start rendering video.
            /// Note, this method may be called multiple times in a row.
            /// </summary>
            public void Start()
            {
                if (this.isRendering)
                    return; // Nothing more to be done

                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    try
                    {
                        Debug.WriteLine("[VideoRenderer::Start] Video rendering setup");
                        StartMediaStreamer();
                        this.isRendering = true;
                    }
                    catch (Exception err)
                    {
                        Debug.WriteLine("[VideoRenderer::Start] " + err.Message);
                    }
                });
            }

            private void StartMediaStreamer()
            {
                if (mediaStreamer == null)
                {
                    mediaStreamer = MediaStreamerFactory.CreateMediaStreamer(123);
                }

                
                int frameWidth = (int)Application.Current.Host.Content.ActualWidth;
                int frameHeight = (int)Application.Current.Host.Content.ActualHeight;
                mediaStreamSource = new VideoStreamSource(null, frameWidth, frameHeight);
                mediaStreamer.SetSource(mediaStreamSource);
                
            }

            /// <summary>
            /// Stop rendering video.
            /// Note, this method may be called multiple times in a row.
            /// </summary>
            public void Stop()
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    if (!this.isRendering)
                        return; // Nothing more to be done

                    Debug.WriteLine("[Video Renderer] Video rendering stopped.");
                    mediaStreamSource = null;
                    mediaStreamer.Dispose();
                    mediaStreamer = null;

                    this.isRendering = false;
                });
            }
    }
}