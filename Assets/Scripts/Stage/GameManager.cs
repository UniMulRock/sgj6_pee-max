using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PeeMax.Stage
{
	public class GameManager : Utility.System.SingletonMonoBehaviour<GameManager> {

		public GameObject PrefabEvent;
		public GameObject PrefabInput;
		public GameObject PrefabAsk;
		public GameObject PrefabDescrib;

		public GameObject PrefabSuccess;
		public GameObject PrefabFail;

		public List<GameObject> SelectedCommands = new List<GameObject>();

		public GoalData GoalRestroom;

		float delayTime = 1f;
		WaitForSeconds delayWait;
		GameObject CharRoot;

		bool isStartedGame = false;
		bool isStartedCommand = false;

		// Use this for initialization
		void Start () {
			isStartedGame = false;
			isStartedCommand = false;

			delayWait = new WaitForSeconds(delayTime);
			StartCoroutine(MainLoop());

			CharRoot = global::System.Scene.GameSceneSystem.Instance.CharRootObject;
			if (CharRoot != null) {
				var start = GameObject.FindWithTag ("Start");
				if (start != null) {
					CharRoot.transform.position = start.transform.position;
					CharRoot.transform.rotation = start.transform.rotation;
				}
			}

			// ゴールをランダムで決定
			var list = GameObject.FindGameObjectsWithTag ("Goal");
			if (list.Length == 0) {
				Debug.LogError ("ERROR : GOALが設定されていません！！");
			}
			int index = (list.Length > 0) ? (int)Random.Range (0, list.Length-1) : 0;
			GoalRestroom = list [index].GetComponent<GoalData> () as GoalData;
		}

		// Update is called once per frame
		void Update () {

			if (isStartedGame && CharRoot != null) {

				// キャラクターへコマンドを伝える
				int count = SelectedCommands.Count;
				var controll = GetCurrentCharController();
				if (count > 0 && controll != null) {
					// コマンドの初期化
					if (isStartedCommand == false) {
						isStartedCommand = true;
						controll.Init (CharRoot);
					}

					// コマンドの実行
					controll.Do(CharRoot);

					// コマンド終了？
					if (controll.IsDone () == true) {
						// 次のコマンド
						isStartedCommand = false;
						PeeMax.Stage.GameManager.Instance.NextCharController ();
						controll = GetCurrentCharController();
						if (controll == null) {
							// 全コマンド実行完了 -> トイレに到達

							StartCoroutine (PhaseSuccessCommand ());
						}
					}
				}
			}

		}
			
		#region API SelectedCommand

		/// <summary>
		/// Inserts the selected command.
		/// </summary>
		/// <param name="cmdObject">Cmd object.</param>
		/// <param name="index">Index.</param>
		public void InsertSelectedCommand(GameObject cmdObject, int index=-1)
		{
			var cmd = cmdObject.GetComponent<PeeMax.Char.CharController> () as PeeMax.Char.CharController;
			if (cmd == null) {
				Debug.LogError ("ERROR : このオブジェクトは追加出来ません！");
				return;
			}

			if (index < 0 || this.SelectedCommands.Count <= index) {
				this.SelectedCommands.Add (GameObject.Instantiate(cmdObject));
			} else {
				GameObject.Destroy (this.SelectedCommands [index]);
				this.SelectedCommands [index] = GameObject.Instantiate(cmdObject);
			}
		}

		/// <summary>
		/// Clears the seleced command.
		/// </summary>
		public void ClearSelecedCommand()
		{
			SelectedCommands.Clear ();
		}

		/// <summary>
		/// Gets the current char controller. 現在実行するコマンドのCharControllerを取得
		/// </summary>
		/// <returns>The current char controller.</returns>
		/// <returns>=null..コマンド終了</returns>
		public PeeMax.Char.CharController GetCurrentCharController()
		{
			if (this.SelectedCommands.Count <= 0) {
				// コマンド終了
				return null;
			}
			return SelectedCommands[0].GetComponent<PeeMax.Char.CharController> () as PeeMax.Char.CharController;
		}

		/// <summary>
		/// Nexts the char controller.
		/// Add IndexOfCurrentCommand+1
		/// </summary>
		public void NextCharController()
		{
			if (this.SelectedCommands.Count > 0) {
				SelectedCommands.RemoveAt (0);
			}
		}

		#endregion

		#region Coroutine

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


		GameObject preObjDescrib = null;
		private IEnumerator PhaseDescribCommand()
		{

			//導入イベント画面
			if (PrefabEvent != null) {
				preObjDescrib = GameObject.Instantiate (PrefabDescrib);
			}

			// 終わるまで待つ
			while (preObjDescrib != null)
			{
				if (Input.anyKey) {
					break;
				}
				yield return null;
			}
			yield return null;
		}

		private IEnumerator PhaseASKCommand()
		{
			GameObject preObj = null;

			//導入イベント画面
			if (PrefabEvent != null) {
				preObj = GameObject.Instantiate (PrefabAsk);
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

		private IEnumerator PhaseSuccessCommand()
		{
			GameObject preObj = null;

			//導入イベント画面
			if (PrefabEvent != null) {
				preObj = GameObject.Instantiate (PrefabSuccess);
			}

			// 終わるまで待つ
			while (preObj != null)
			{
				if (Input.anyKey) {
					GameObject.Destroy (preObj);

					Utility.System.Sound.StopBgm ();
					Utility.System.SceneManager.Instance.ChangeState(Utility.System.SceneManager.STATE.STAGE_SELECT);
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
					if (preObjDescrib != null) {
						GameObject.Destroy (preObjDescrib);
					}
					GameObject.Destroy (preObj);
					break;
				}
				yield return null;
			}
			yield return null;
		}

		#endregion

		private IEnumerator MainLoop()
		{
			while (true) {

				yield return delayWait;

				// 導入イベント
				yield return StartCoroutine (PhaseEventCommand ());
				yield return delayWait;

				// 案内イベント
				yield return StartCoroutine (PhaseDescribCommand ());
				yield return delayWait;

				// ASKイベント
				yield return StartCoroutine (PhaseASKCommand ());
				yield return delayWait;

				// コマンド入力
				yield return StartCoroutine (PhaseInputCommand ());
				yield return delayWait;

				isStartedGame = true;

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
