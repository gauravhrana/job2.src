IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AccidentPlaceDelete')
BEGIN
	PRINT 'Dropping Procedure AccidentPlaceDelete'
	DROP  Procedure  AccidentPlaceDelete
END
GO

PRINT 'Creating Procedure AccidentPlaceDelete'
GO

/******************************************************************************
**		File: 
**		Name: AccidentPlaceDelete
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

CREATE Procedure dbo.AccidentPlaceDelete
(
		@AccidentPlaceId		INT     
	,	@AuditId				INT			  
	,	@AuditDate				DATETIME		= NULL
	,	@SystemEntityType		VARCHAR(50)		= 'AccidentPlace'
)
AS
BEGIN

	DELETE	dbo.AccidentPlace
	WHERE	AccidentPlaceId = @AccidentPlaceId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @AccidentPlaceId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO

