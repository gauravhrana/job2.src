IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'HelpPageContextUpdate')
BEGIN
	PRINT 'Dropping Procedure HelpPageContextUpdate'
	DROP  Procedure  HelpPageContextUpdate
END
GO

PRINT 'Creating Procedure HelpPageContextUpdate'
GO

/******************************************************************************
**		File: 
**		Name: HelpPageContextUpdate
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
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.HelpPageContextUpdate
(
		@HelpPageContextId		INT 
	,	@ApplicationId			INT				
	,	@Name					VARCHAR(50)				
	,	@Description            VARCHAR (500)			
	,	@SortOrder				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'HelpPageContext'
)
AS
BEGIN 

	UPDATE	dbo.HelpPageContext 
	SET		Name				=	@Name
		,	ApplicationId		= 	@ApplicationId	
		,	Description			=	@Description				
		,	SortOrder			=	@SortOrder					
	WHERE	HelpPageContextId	=	@HelpPageContextId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @HelpPageContextId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

 END		
 GO
