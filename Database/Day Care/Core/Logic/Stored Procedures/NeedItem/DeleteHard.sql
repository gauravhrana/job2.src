IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NeedItemDeleteHard')
BEGIN
	PRINT 'Dropping Procedure NeedItemDeleteHard'
	DROP  Procedure NeedItemDeleteHard
END
GO

PRINT 'Creating Procedure NeedItemDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: NeedItemDeleteHard
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

CREATE Procedure dbo.NeedItemDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'NeedItem'			

)
AS
BEGIN

	IF @KeyType = 'NeedItemId'
		BEGIN 

			EXEC	dbo.NeedsDeleteHard 
					@KeyId		=	@KeyId 
				,	@KeyType	=	'NeedItemId'
				,	@AuditId 	=	@AuditId
	
			DELETE	dbo.NeedItem
			WHERE	NeedItemId = @KeyId

		END

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType			= @SystemEntityType
		,	@EntityKey					= @KeyId
		,	@AuditAction				= 'DeleteHard'
		,	@CreatedDate				= @AuditDate
		,	@CreatedByPersonId			= @AuditId
		
END
GO
