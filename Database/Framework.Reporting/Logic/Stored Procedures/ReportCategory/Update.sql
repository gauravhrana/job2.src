IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReportCategoryUpdate')
BEGIN
	PRINT 'Dropping Procedure ReportCategoryUpdate'
	DROP  Procedure  ReportCategoryUpdate
END
GO

PRINT 'Creating Procedure ReportCategoryUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ReportCategoryUpdate
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
Create Procedure dbo.ReportCategoryUpdate
(
			@ReportCategoryId		INT		 			
		,	@Name					VARCHAR(50)			
		,	@Description            VARCHAR(500)			
		,	@SortOrder				INT	  
		,	@ApplicationId			INT	
		,	@AuditId				INT					
		,	@AuditDate				DATETIME	= NULL	
		,	@SystemEntityType		VARCHAR(50)	= 'ReportCategory'
)
AS
BEGIN 
	DECLARE		@ModifiedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@ModifiedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE	dbo.ReportCategory 
	SET		Name			=	@Name		
		,	Description		=	@Description				
		,	SortOrder		=	@SortOrder
		,	ApplicationId		=	@ApplicationId	
		,   ModifiedDate		=	@ModifiedDate
		,	ModifiedByAuditId	=   @ModifiedByAuditId											
	WHERE	ReportCategoryId=	@ReportCategoryId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReportCategory'
		,	@EntityKey				= @ReportCategoryId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO



