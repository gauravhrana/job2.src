IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileSetDeleteHard')
BEGIN
	PRINT 'Dropping Procedure BatchFileSetDeleteHard'
	DROP  Procedure BatchFileSetDeleteHard
END
GO

PRINT 'Creating Procedure BatchFileSetDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: BatchFileSetDelete
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
CREATE Procedure dbo.BatchFileSetDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50) = 'BatchFileSet'				
)
AS
BEGIN

	IF @KeyType = 'BatchFileSetId'
	BEGIN

		EXEC	dbo.BatchFileHistoryDeleteHard 
				@KeyId		=	@KeyId, 
				@KeyType	=	'BatchFileSetId',
				@AuditId	=	@AuditId

		EXEC	dbo.BatchFileDeleteHard 
				@KeyId		=	@KeyId, 
				@KeyType	=	'BatchFileSetId',
				@AuditId	=	@AuditId

		DELETE	 dbo.BatchFileSet
		WHERE	 BatchFileSetId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
