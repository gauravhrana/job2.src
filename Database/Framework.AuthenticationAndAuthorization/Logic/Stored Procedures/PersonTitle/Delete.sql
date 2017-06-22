IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'PersonTitleDelete')
BEGIN
	PRINT 'Dropping Procedure PersonTitleDelete'
	DROP  Procedure PersonTitleDelete
END
GO

PRINT 'Creating Procedure PersonTitleDelete'
GO
/******************************************************************************
**		File: 
**		Name: PersonTitleDelete
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
CREATE Procedure dbo.PersonTitleDelete
(
		@PersonTitleId 		INT						
	,	@AuditId			INT						
	,	@AuditDate			DATETIME	= NULL		
	,	@SystemEntityType	VARCHAR(50)	= 'PersonTitle'
)
AS
BEGIN

	DELETE	 dbo.PersonTitle
	WHERE	 PersonTitleId = @PersonTitleId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @PersonTitleId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
