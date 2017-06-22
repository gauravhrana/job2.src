IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileHistoryDeleteHard')
BEGIN
	PRINT 'Dropping Procedure BatchFileHistoryDeleteHard'
	DROP  Procedure BatchFileHistoryDeleteHard
END
GO

PRINT 'Creating Procedure BatchFileHistoryDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: BatchFileHistoryDelete
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
CREATE Procedure dbo.BatchFileHistoryDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'BatchFileHistory'
)
AS
BEGIN

	IF @KeyType = 'BatchFileHistoryId'
		BEGIN

			DELETE	 dbo.BatchFileHistory
			WHERE	 BatchFileHistoryId = @KeyId

		END
	ELSE IF @KeyType = 'BatchFileSetId'
		BEGIN

			DELETE	 dbo.BatchFileHistory
			WHERE	 BatchFileSetId = @KeyId

		END
	ELSE IF @KeyType = 'BatchFileStatusId'
		BEGIN

			DELETE	 dbo.BatchFileHistory
			WHERE	 BatchFileStatusId = @KeyId

		END
	ELSE IF @KeyType = 'BatchFileId'
		BEGIN

			DELETE	 dbo.BatchFileHistory
			WHERE	 BatchFileId = @KeyId

		END
	ELSE IF @KeyType = 'UpdatedByPersonId'
		BEGIN

			DELETE	 dbo.BatchFileHistory
			WHERE	 UpdatedByPersonId = @KeyId

	
	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
