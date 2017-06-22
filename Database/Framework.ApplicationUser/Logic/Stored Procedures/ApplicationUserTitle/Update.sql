--IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserTitleUpdate')
--BEGIN
--	PRINT 'Dropping Procedure ApplicationUserTitleUpdate'
--	DROP  Procedure  ApplicationUserTitleUpdate
--END
--GO

--PRINT 'Creating Procedure ApplicationUserTitleUpdate'
--GO

/******************************************************************************
**		File: 
**		Name: ApplicationUserTitleUpdate
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

CREATE Procedure dbo.ApplicationUserTitleUpdate
(
		@ApplicationUserTitleId		INT	
	,	@ApplicationId				INT		 			
	,	@Name						VARCHAR(50)					
	,	@Description				VARCHAR(50)				
	,	@SortOrder					INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'ApplicationUserTitle'	
)
AS
BEGIN 

	UPDATE  dbo.ApplicationUserTitle 
	SET		ApplicationId	=	@ApplicationId
		,	Name			=	@Name				
		,	Description		=	@Description				
		,	SortOrder		=	@SortOrder							
	WHERE	ApplicationUserTitleId	=	@ApplicationUserTitleId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @ApplicationUserTitleId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		

 GO