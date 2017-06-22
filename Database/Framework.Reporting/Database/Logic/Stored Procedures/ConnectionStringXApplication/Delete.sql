IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ConnectionStringXApplicationDelete')
BEGIN
	PRINT 'Dropping Procedure ConnectionStringXApplicationDelete'
	DROP  Procedure ConnectionStringXApplicationDelete
END
GO

PRINT 'Creating Procedure ConnectionStringXApplicationDelete'
GO
/******************************************************************************
**		File: 
**		Name: ConnectionStringXApplicationDelete
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
CREATE Procedure dbo.ConnectionStringXApplicationDelete
(
		@ConnectionStringXApplicationId				INT			= NULL	
	,	@ApplicationId								INT			= NULL	
	,	@ConnectionStringId							INT			= NULL	
	,	@AuditId									INT					
	,	@AuditDate									DATETIME	= NULL
	,	@SystemEntityType							VARCHAR(50)	= 'ConnectionStringXApplication'
)
AS
BEGIN

	DELETE	dbo.ConnectionStringXApplication
	WHERE	ConnectionStringXApplicationId	=	ISNULL(@ConnectionStringXApplicationId,	ConnectionStringXApplicationId)	
	AND		ApplicationId					=	ISNULL(@ApplicationId,			ApplicationId)
	AND		ConnectionStringId				=	ISNULL(@ConnectionStringId,			ConnectionStringId)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ConnectionStringXApplicationId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
