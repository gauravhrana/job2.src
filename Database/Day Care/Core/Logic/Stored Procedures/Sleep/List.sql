IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SleepList')
BEGIN
	PRINT 'Dropping Procedure SleepList'
	DROP PROCEDURE SleepList
END
GO

PRINT 'Creating Procedure SleepList'
GO

/******************************************************************************
**		File: 
**		Name: SleepList
**		Desc: 
**
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**		----------						-----------
**
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:			Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.SleepList
(
		@AuditId				INT	
	,	@ApplicationId			INT			= NULL
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Sleep'
)
AS
BEGIN
		SELECT	a.*
		FROM	dbo.Sleep a
		WHERE ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
		ORDER BY SleepId			ASC

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
