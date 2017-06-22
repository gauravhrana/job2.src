IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserPreferenceTopNPreference')
BEGIN
	PRINT 'Dropping Procedure UserPreferenceTopNPreference'
	DROP  Procedure UserPreferenceTopNPreference
END
GO

PRINT 'Creating Procedure UserPreferenceTopNPreference'
GO


/******************************************************************************
**		File: 
**		PersonId: UserPreferenceTopNPreference
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------							-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.UserPreferenceTopNPreference
(
		@ApplicationId			INT					
	,	@UserPreferenceKeyId	INT					
	,	@Value					VARCHAR(50)			
	,	@TopN					INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityTypeId		INT			= 1200	
	,	@SystemEntityType		VARCHAR(50) = 'UserPreference'
)
AS
BEGIN

	CREATE TABLE #TempMain
	(	
			UserPreferenceKeyId			INT
		,	UserPreferenceKey			VARCHAR(50)		
		,	Value						VARCHAR(50)						
		,	Count						INT
	)

	SET ROWCOUNT @TopN

	INSERT INTO #TempMain
	SELECT	a.UserPreferenceKeyId
		,	b.Name					AS 'UserPreferenceKey'
		,	a.Value
		,	COUNT(*)				AS 'Count' 
	FROM		UserPreference a
	INNER JOIN	UserPreferenceKey b ON b.UserPreferenceKeyId = a.UserPreferenceKeyId
	WHERE	a.ApplicationId			= @ApplicationId
	AND		a.UserPreferenceKeyId	= @UserPreferenceKeyId
	GROUP BY	a.UserPreferenceKeyId
		,		b.Name
		,		a.Value
	ORDER BY 'Count'  DESC
			, a.Value ASC			

	SET ROWCOUNT 0

	DECLARE @CntTMP AS INT = 0

	SELECT	@CntTMP = COUNT(*)
	FROM	#TempMain a
	WHERE	a.Value = @Value

	IF @CntTMP = 0
		BEGIN

			INSERT INTO #TempMain
			SELECT	a.UserPreferenceKeyId
				,	b.Name					AS 'UserPreferenceKey'
				,	a.Value
				,	COUNT(*)				AS 'Count' 
			FROM		UserPreference a
			INNER JOIN	UserPreferenceKey b ON b.UserPreferenceKeyId = a.UserPreferenceKeyId
			WHERE	a.ApplicationId = @ApplicationId
			AND		a.UserPreferenceKeyId = @UserPreferenceKeyId
			AND		a.Value = @Value
			GROUP BY	a.UserPreferenceKeyId
				,		b.Name
				,		a.Value

		END

	SELECT	a.UserPreferenceKeyId
		,	a.UserPreferenceKey	
		,	a.Value
		,	a.Count
	FROM	#TempMain a

		
END
GO
   