IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AccidentPlaceDeleteHard')
BEGIN
	PRINT 'Dropping Procedure AccidentPlaceDeleteHard'
	DROP  Procedure AccidentPlaceDeleteHard
END
GO

PRINT 'Creating Procedure AccidentPlaceDeleteHard'
GO


/******************************************************************************
**		File: 
**		Name: AccidentPlaceDeleteHard
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

CREATE Procedure dbo.AccidentPlaceDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL		
	,	@SystemEntityType		VARCHAR(50)		= 'AccidentPlace'
)
AS
BEGIN

	IF @KeyType = 'AccidentPlaceId'
		BEGIN 

			EXEC	dbo.AccidentReportDeleteHard 
					@KeyId		=	@KeyId 
				,	@KeyType	=	'AccidentPlaceId'
				,	@AuditId 	=	@AuditId
	
			DELETE	dbo.AccidentPlace
			WHERE	AccidentPlaceId = @KeyId

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
