using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
public class LeaderBoardHandler : MonoBehaviour
{
	#region PUBLIC_VAR
	public string highScoreLeaderboardId;
	public string playerId;
	public long leaderboardHighScore;

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
		LogIn ();
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
					playerId = Social.localUser.id;
				} else {
					Debug.Log ("Login failed");
				}
			});
		ShowHighScoreLeaderBoard ();
	}
	/// <summary>
	/// Shows All Available Leaderborad
	/// </summary>
	public void ShowHighScoreLeaderBoard ()
	{
		//        Social.ShowLeaderboardUI (); // Show all leaderboard
		Social.LoadScores (highScoreLeaderboardId, scores => {
			if (scores.Length > 0) 
			{
				PlayGamesPlatform.Instance.LoadScores (highScoreLeaderboardId, LeaderboardStart.PlayerCentered,1, LeaderboardCollection.Public, LeaderboardTimeSpan.AllTime,
					(LeaderboardScoreData data) => {
					});
			}
			else
			{
				OnAddScoreToLeaderBoard(GameManager.score);
			}
		});
	   ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI (highScoreLeaderboardId);// Show current (Active) leaderboard
	}
	/// <summary>
	/// Adds Score To leader board
	/// </summary>
	/// 
	public long GetPlayerHighScore()
	{
		Social.LoadScores (highScoreLeaderboardId, scores => {
			if (scores.Length > 0) {
				PlayGamesPlatform.Instance.LoadScores (highScoreLeaderboardId, LeaderboardStart.PlayerCentered, 1, LeaderboardCollection.Public, LeaderboardTimeSpan.AllTime,
					(LeaderboardScoreData data) => {
						leaderboardHighScore = data.PlayerScore.value;
					});
			} else {
				leaderboardHighScore = 0;
			}
		});
		return leaderboardHighScore; 
	}
	public void OnAddScoreToLeaderBoard (int score)
	{
		if (Social.localUser.authenticated) {
			Social.ReportScore (score, highScoreLeaderboardId, (bool success) =>
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

   	