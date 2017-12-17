using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PeeMax.System
{

	public class GameManager : Utility.System.SingletonMonoBehaviour<GameManager> {

		public GameObject PrefabEvent;
		public GameObject PrefabInput;

		public List<GameObject> SelectedCommands = new List<GameObject>();

		public int IndexOfCurrentCommand;

		public float delayTime = 2f;
		private WaitForSeconds delayWait;


		// Use this for initialization
		void Start () {
			IndexOfCurrentCommand = 0;

			delayWait = new WaitForSeconds(delayTime);
			StartCoroutine(MainLoop());
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

		/// <summary>
		/// 導入イベント
		/// </summary>
		/// <returns>The event command.</returns>
		private IEnumerator PhaseEventCommand()
		{
			GameObject preObj = null;

			//導入イベント画面
			if (PrefabEvent != null) {
				preObj = GameObject.Instantiate (PrefabEvent);
			}

			// 終わるまで待つ
			while (preObj != null)
			{
				if (Input.anyKey) {
					GameObject.Destroy (preObj);
					break;
				}
				yield return null;
			}
			yield return null;
		}

		/// <summary>
		/// コマンド入力
		/// </summary>
		/// <returns>The input command.</returns>
		private IEnumerator PhaseInputCommand()
		{
			GameObject preObj = null;
			EndInputCommand inpObj = null;

			//Command Input
			if (PrefabInput != null) {
				preObj = GameObject.Instantiate (PrefabInput);
				inpObj = preObj.GetComponent<EndInputCommand> () as EndInputCommand;
			}

			// 終わるまで待つ
			while (preObj != null)
			{
				if (inpObj.IsEndOfInputCommand) {
					GameObject.Destroy (preObj);
					break;
				}
				yield return null;
			}
			yield return null;
		}

		private IEnumerator MainLoop()
		{
			while (true) {

				yield return delayWait;

				// 導入イベント
				yield return StartCoroutine (PhaseEventCommand ());

				// コマンド入力
				yield return StartCoroutine (PhaseInputCommand ());

				yield return delayWait;

				//ゲーム終了まで末
				while (true) {

					// ゴール到達したか？

					// 成功？失敗？

					// ステージセレクト、エンディング、タイトルへ

					yield return null;
				}
			}
		}
	}

}
