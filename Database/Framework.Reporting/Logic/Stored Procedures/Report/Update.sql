IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReportUpdate')
BEGIN
	PRINT 'Dropping Procedure ReportUpdate'
	DROP  Procedure  ReportUpdate
END
GO

PRINT 'Creating Procedure ReportUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ReportUpdate
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
Create Procedure dbo.ReportUpdate
(
			@ReportId				INT	
	  	,	@ApplicationId			INT		 			
		,	@Name					VARCHAR(50)			
		,	@Description            VARCHAR(500)
		,	@Title					VARCHAR(50)			
		,	@SortOrder				INT									
		,	@AuditId				INT					
		,	@AuditDate				DATETIME	= NULL	
		,	@SystemEntityType		VARCHAR(50)	= 'Report'
)
AS
BEGIN 

	DECLARE		@ModifiedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@ModifiedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE	dbo.Report 
	SET		Name			=	@Name		
		,	Description		=	@Description
		,	Title           =   @Title				
		,	SortOrder		=	@SortOrder
		, 	ApplicationId	=	@ApplicationId 
		,   ModifiedDate		=	@ModifiedDate
		,	ModifiedByAuditId	=   @ModifiedByAuditId			
	WHERE	ReportId		=	@ReportId
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'Report'
		,	@EntityKey				= @ReportId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO