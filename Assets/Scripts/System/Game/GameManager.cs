using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PeeMax.System
{

	public class GameManager : Utility.System.SingletonMonoBehaviour<GameManager> {


		public List<GameObject> SelectedCommands = new List<GameObject>();

		public int IndexOfCurrentCommand;


		// Use this for initialization
		void Start () {
			IndexOfCurrentCommand = 0;
		}

		// Update is called once per frame
		void Update () {

		}
			
		/// <summary>
		/// Inserts the selected command.
		/// </summary>
		/// <param name="cmdObject">Cmd object.</param>
		/// <param name="index">Index.</param>
		void InsertSelectedCommand(GameObject cmdObject, int index=-1)
		{
			var cmd = cmdObject.GetComponent<PeeMax.Char.CharController> () as PeeMax.Char.CharController;
			if (cmd == null) {
				Debug.LogError ("ERROR : このオブジェクトは追加出来ません！");
				return;
			}

			if (index < 0 || this.SelectedCommands.Count <= index) {
				this.SelectedCommands.Add (cmdObject);
			} else {
				this.SelectedCommands [index] = cmdObject;
			}
		}

		/// <summary>
		/// Clears the seleced command.
		/// </summary>
		void ClearSelecedCommand()
		{
			SelectedCommands.Clear ();
			IndexOfCurrentCommand = 0;
		}

		/// <summary>
		/// Gets the current char controller. 現在実行するコマンドのCharControllerを取得
		/// </summary>
		/// <returns>The current char controller.</returns>
		/// <returns>=null..コマンド終了</returns>
		public PeeMax.Char.CharController GetCurrentCharController()
		{
			if (this.SelectedCommands.Count <= IndexOfCurrentCommand) {
				// コマンド終了
				return null;
			}
			return SelectedCommands[IndexOfCurrentCommand].GetComponent<PeeMax.Char.CharController> () as PeeMax.Char.CharController;
		}

		/// <summary>
		/// Nexts the char controller.
		/// Add IndexOfCurrentCommand+1
		/// </summary>
		public void NextCharController()
		{
			if (this.SelectedCommands.Count > IndexOfCurrentCommand) {
				IndexOfCurrentCommand++;
			}
		}

	}

}
