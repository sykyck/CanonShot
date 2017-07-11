using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
public class LeaderBoardHandler : MonoBehaviour
{
	#region PUBLIC_VAR
	public string leaderboard;
	public string id;
	public long highScore;
	private static LeaderBoardHandler instance;

	private LeaderBoardHandler() {}

	public static LeaderBoardHandler GetInstance()
	{
		if (instance == null)
		{
			instance = new LeaderBoardHandler();
		}
		return instance;
	}
	#endregion
	#region DEFAULT_UNITY_CALLBACKS
	void Start ()
	{
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;

		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate ();
	}
	#endregion
	#region BUTTON_CALLBACKS
	/// <summary>
	/// Login In Into Your Google+ Account
	/// </summary>
	public void LogIn ()
	{
		Social.localUser.Authenticate ((bool success) =>
			{
				if (success) {
					Debug.Log ("Login Sucess");
				} else {
					Debug.Log ("Login failed");
				}
			});
		OnShowLeaderBoard ();
	}
	/// <summary>
	/// Shows All Available Leaderborad
	/// </summary>
	public void OnShowLeaderBoard ()
	{
		//        Social.ShowLeaderboardUI (); // Show all leaderboard
		((PlayGamesPlatform)Social.Active).ShowLeaderboardUI (leaderboard); // Show current (Active) leaderboard

	}
	/// <summary>
	/// Adds Score To leader board
	/// </summary>
	/// 
	public void GetPlayerHighScore()
	{
		PlayGamesPlatform.Instance.LoadScores (leaderboard,LeaderboardStart.PlayerCentered,1,LeaderboardCollection.Public,LeaderboardTimeSpan.AllTime,
			(LeaderboardScoreData data) => {
				Debug.Log (data.Valid);
				Debug.Log (data.Id);
				Debug.Log (data.PlayerScore);
				Debug.Log (data.PlayerScore.userID);
				Debug.Log (data.PlayerScore.formattedValue);
				id = data.PlayerScore.userID;
				highScore=data.PlayerScore.value;
			});
	}
	public void OnAddScoreToLeaderBoard (int score)
	{
		if (Social.localUser.authenticated) {
			Social.ReportScore (score, leaderboard, (bool success) =>
				{
					if (success) {
						Debug.Log ("Update Score Success");

					} else {
						Debug.Log ("Update Score Fail");
					}
				});
		}
	}
	public void RemoveHighScore()
	{
		PlayGamesPlatform.Instance.LoadScores (leaderboard,LeaderboardStart.PlayerCentered,1,LeaderboardCollection.Public,LeaderboardTimeSpan.AllTime,
			(LeaderboardScoreData data) => {
				highScore=0;
			});
	}	
	/// <summary>
	/// On Logout of your Google+ Account
	/// </summary>
	public void OnLogOut ()
	{
		((PlayGamesPlatform)Social.Active).SignOut ();
	}
	#endregion
}