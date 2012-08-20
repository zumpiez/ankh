﻿using System.Windows.Forms;
using Ankh;

namespace Defenetron
{
	class AnkhApp : Form
	{
		private readonly DefenetronGame game;

		AnkhApp()
		{
			// As of this writing, the DX11 renderer crashes when creating a Texture2D.
			//GraphicsDeviceBase device = GraphicsDevice.Create(this);
			var device = new Ankh.DX9.GraphicsDevice(this);
			game = new DefenetronGame(device);
		}

		public void RenderLoop()
		{
			game.Render();

		}

		static void Main(string[] args)
		{
			var frame = new Form();
			var device = new Ankh.DX9.GraphicsDevice(frame);
			var game = new DefenetronGame(device);
			var m = new MessageLoop();
			m.Run(game);
		}
	}
}
