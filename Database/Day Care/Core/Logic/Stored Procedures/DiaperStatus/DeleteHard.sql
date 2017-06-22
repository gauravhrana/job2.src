IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DiaperStatusDeleteHard')
BEGIN
	PRINT 'Dropping Procedure DiaperStatusDeleteHard'
	DROP  Procedure DiaperStatusDeleteHard
END
GO

PRINT 'Creating Procedure DiaperStatusDeleteHard'
GO

/******************************************************************************
**		File: 
**		Name: DiaperStatusDeleteHard
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
**     ----------						-----------
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
CREATE Procedure dbo.DiaperStatusDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'DiaperStatus'
)
AS
BEGIN

	IF @KeyType = 'DiaperStatusId'
	BEGIN

		EXEC dbo.BathroomDeleteHard 
				@KeyId		=	@KeyId 
			,	@KeyType	=	'DiaperStatusId'
			,	@AuditId 	=	@AuditId

		DELETE	 dbo.DiaperStatus
		WHERE	 DiaperStatusId = @KeyId

	END

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @KeyId
		,	@AuditAction			= 'DeleteHard'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
