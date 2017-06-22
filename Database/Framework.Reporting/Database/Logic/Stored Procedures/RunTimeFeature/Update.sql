IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'RunTimeFeatureUpdate')
BEGIN
	PRINT 'Dropping Procedure RunTimeFeatureUpdate'
	DROP  Procedure  RunTimeFeatureUpdate
END
GO

PRINT 'Creating Procedure RunTimeFeatureUpdate'
GO

/******************************************************************************
**		File: 
**		Name: RunTimeFeatureUpdate
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

CREATE Procedure dbo.RunTimeFeatureUpdate
(
		@RunTimeFeatureId			INT				= NULL	 			
	,	@Name						VARCHAR(50)				
	,	@Description				VARCHAR(500)			
	,	@SortOrder					INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'RunTimeFeature'
)
AS
BEGIN
	UPDATE	dbo.RunTimeFeature 
	SET		Name					=	@Name				
		,	Description				=	@Description				
		,	SortOrder				=	@SortOrder							
	WHERE	RunTimeFeatureId	=	@RunTimeFeatureId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'RunTimeFeature'
		,	@EntityKey				= @RunTimeFeatureId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO