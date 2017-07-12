using UnityEngine;
using System.Collections;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
public class LeaderBoardHandler : MonoBehaviour
{
	public string highScoreLeaderboardId;
	public string playerId;

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
	void Start ()
	{
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		highScoreLeaderboardId = "CgkIkvGAt_UJEAIQAQ";
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate ();
		LogIn ();
	}
	/// <summary>
	/// Login In Into Your Google+ Account
	/// </summary>
	public void LogIn ()
	{
		PlayGamesPlatform.Instance.localUser.Authenticate ((bool success) =>
			{
				if (success) {
					playerId = Social.localUser.id;
					GetPlayerHighScore();
				} else {
				}
			});
	}
	/// <summary>
	/// Shows All Available Leaderborad
	/// </summary>
	public void ShowHighScoreLeaderBoard ()
	{
		//        Social.ShowLeaderboardUI (); // Show all leaderboard
		((PlayGamesPlatform)Social.Active).ShowLeaderboardUI ("CgkIkvGAt_UJEAIQAQ", LeaderboardTimeSpan.AllTime, (status) => {
			if (status==UIStatus.Valid) {
				Debug.Log ("valid");
			}
		});// Show current (Active) leaderboard
	}
	/// <summary>
	/// Adds Score To leader board
	/// </summary>
	/// 
	public long GetPlayerHighScore()
	{
		PlayGamesPlatform.Instance.LoadScores ("CgkIkvGAt_UJEAIQAQ", LeaderboardStart.PlayerCentered, 1, LeaderboardCollection.Social, LeaderboardTimeSpan.AllTime,
			(LeaderboardScoreData data) => {
				if(data.Scores.Length>0)
				{
					GameManager.leaderBoardHighScore = data.PlayerScore.value;
				}
				else
				{
					PlayGamesPlatform.Instance.ReportScore(0,"CgkIkvGAt_UJEAIQAQ",(bool success) => {
						if (success) {
						} else {
						}
					    GameManager.leaderBoardHighScore = 0;
					});
				}
			});
	    return (GameManager.leaderBoardHighScore); 
	}
	public void OnAddScoreToLeaderBoard (int score)
	{
		if (PlayGamesPlatform.Instance.localUser.authenticated) {
			PlayGamesPlatform.Instance.ReportScore (score,"CgkIkvGAt_UJEAIQAQ", (bool success) => {
				if (success) {
				} else {
				}
			});
		}
	}

	/// <summary>
	/// On Logout of your Google+ Account
	/// </summary>
	public void OnLogOut ()
	{
		((PlayGamesPlatform)Social.Active).SignOut ();
	}
}

   	