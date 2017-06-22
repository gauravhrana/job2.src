IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'FeatureOwnerStatusUpdate')
BEGIN
	PRINT 'Dropping Procedure FeatureOwnerStatusUpdate'
	DROP  Procedure  FeatureOwnerStatusUpdate
END
GO

PRINT 'Creating Procedure FeatureOwnerStatusUpdate'
GO

/******************************************************************************
**		File: 
**		Name: FeatureOwnerStatusUpdate
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

CREATE Procedure dbo.FeatureOwnerStatusUpdate
(
		@FeatureOwnerStatusId			INT				= NULL	 			
	,	@Name						VARCHAR(50)				
	,	@Description				VARCHAR(50)			
	,	@SortOrder					INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'FeatureOwnerStatus'
)
AS
BEGIN
	UPDATE	dbo.FeatureOwnerStatus 
	SET		Name					=	@Name				
		,	Description				=	@Description				
		,	SortOrder				=	@SortOrder							
	WHERE	FeatureOwnerStatusId	=	@FeatureOwnerStatusId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'FeatureOwnerStatus'
		,	@EntityKey				= @FeatureOwnerStatusId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO