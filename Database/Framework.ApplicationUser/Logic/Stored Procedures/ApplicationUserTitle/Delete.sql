--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserTitleDelete')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserTitleDelete'
--	DROP  Procedure ApplicationUserTitleDelete
--END
--GO

--PRINT 'Creating Procedure ApplicationUserTitleDelete'
--GO

/******************************************************************************
**		File: 
**		Name: ApplicationUserTitleDelete
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
CREATE Procedure dbo.ApplicationUserTitleDelete
(
		@ApplicationUserTitleId 		INT						
	,	@AuditId			INT						
	,	@AuditDate			DATETIME	= NULL		
	,	@SystemEntityType	VARCHAR(50)	= 'ApplicationUserTitle'
)
AS
BEGIN

	DELETE	 dbo.ApplicationUserTitle
	WHERE	 ApplicationUserTitleId = @ApplicationUserTitleId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType				= @SystemEntityType
		,	@EntityKey						= @ApplicationUserTitleId
		,	@AuditAction					= 'Delete' 
		,	@CreatedDate					= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
