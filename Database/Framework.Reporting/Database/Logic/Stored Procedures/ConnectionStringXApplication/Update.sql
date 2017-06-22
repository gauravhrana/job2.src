IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ConnectionStringXApplicationUpdate')
BEGIN
	PRINT 'Dropping Procedure ConnectionStringXApplicationUpdate'
	DROP  Procedure  ConnectionStringXApplicationUpdate
END
GO

PRINT 'Creating Procedure ConnectionStringXApplicationUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ConnectionStringXApplicationUpdate
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

CREATE Procedure dbo.ConnectionStringXApplicationUpdate
(
		@ConnectionStringXApplicationId				INT	
	,	@ApplicationId								INT					
	,	@ConnectionStringId							INT					
	,	@AuditId									INT					
	,	@AuditDate									DATETIME	= NULL	
	,	@SystemEntityType							VARCHAR(50)	= 'ConnectionStringXApplication'
)
AS
BEGIN 

	UPDATE	dbo.ConnectionStringXApplication 
	SET		ApplicationId						=	@ApplicationId		
		,	ConnectionStringId					=	@ConnectionStringId							
	WHERE	ConnectionStringXApplicationId		=	@ConnectionStringXApplicationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @ConnectionStringXApplicationId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO