IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileDeleteHard')
BEGIN
	PRINT 'Dropping Procedure BatchFileDeleteHard'
	DROP  Procedure BatchFileDeleteHard
END
GO

PRINT 'Creating Procedure BatchFileDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: BatchFileDeleteHard
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

CREATE Procedure dbo.BatchFileDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType	    VARCHAR(50)	='BatchFile'	
)
AS
BEGIN	
	
	IF @KeyType = 'BatchFileId'
		BEGIN
	
			DELETE	 dbo.BatchFile
			WHERE	 BatchFileId = @KeyId

		END
	ELSE IF @KeyType = 'FileTypeId'
		BEGIN
	
			DELETE	 dbo.BatchFile
			WHERE	 FileTypeId = @KeyId

		END
	ELSE IF @KeyType = 'BatchFileSetId'
		BEGIN
	
			DELETE	 dbo.BatchFile
			WHERE	 BatchFileSetId = @KeyId

		END
	ELSE IF @KeyType = 'BatchFileStatusId'
		BEGIN
	
			DELETE	 dbo.BatchFile
			WHERE	 BatchFileStatusId = @KeyId

		END
	ELSE IF @KeyType = 'SystemEntityTypeId'
		BEGIN
	
			DELETE	 dbo.BatchFile
			WHERE	 SystemEntityTypeId = @KeyId

		END
	ELSE IF @KeyType = 'PersonId'
		BEGIN
	
			DELETE	 dbo.BatchFile
			WHERE	 CreatedByPersonId		= @KeyId		
			OR		 UpdatedByPersonId		= @KeyId


	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
