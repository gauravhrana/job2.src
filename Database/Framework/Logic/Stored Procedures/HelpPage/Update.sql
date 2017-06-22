IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'HelpPageUpdate')
BEGIN
	PRINT 'Dropping Procedure HelpPageUpdate'
	DROP  Procedure  HelpPageUpdate
END
GO

PRINT 'Creating Procedure HelpPageUpdate'
GO

/******************************************************************************
**		File: 
**		Name: HelpPageUpdate
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
**		Date:		Author:				Content:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.HelpPageUpdate
(
		@HelpPageId					INT
	,	@Name						VARCHAR(50)				
	,	@SystemEntityTypeId			INT					
	,	@HelpPageContextId			INT					
	,	@Content					VARCHAR(MAX)						
	,	@SortOrder					INT			
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'HelpPage'
)
AS
BEGIN

	UPDATE	dbo.HelpPage 
	SET		Name					=	@Name				
		,	Content					=	@Content				
		,	SortOrder				=	@SortOrder
		,	SystemEntityTypeId		=	@SystemEntityTypeId		
		,	HelpPageContextId		=	@HelpPageContextId
	WHERE	HelpPageId				=	@HelpPageId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @HelpPageId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

 END		
 GO
