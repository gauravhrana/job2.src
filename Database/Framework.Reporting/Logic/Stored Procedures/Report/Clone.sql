IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReportClone')
BEGIN
	PRINT 'Dropping Procedure ReportClone'
	DROP  Procedure ReportClone
END
GO

PRINT 'Creating Procedure ReportClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ReportClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/
CREATE Procedure dbo.ReportClone
(
			@ReportId				INT				= NULL 	OUTPUT		
		,	@Name					VARCHAR(50)
		,	@Description            VARCHAR(500)
		,	@Title					VARCHAR(50)						
		,	@SortOrder				INT	
		,	@CreatedDate			DATETIME	= NULL
		,	@ModifiedDate			DATETIME	= NULL
		,	@CreatedByAuditId		INT			= NULL
		,	@ModifiedByAuditId		INT			= NULL							
		,	@AuditId				INT									
		,	@AuditDate				DATETIME		= NULL			
		,	@SystemEntityType		VARCHAR(50)		= 'Report'
)
AS
BEGIN
		IF @ReportId IS NULL OR @ReportId = -999999
		BEGIN
			EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'Report', @ReportId OUTPUT
		END			
	
		SELECT	@Description		= Description
			,	@SortOrder			= SortOrder
			,	@CreatedDate		= CreatedDate
			,	@ModifiedDate		= ModifiedDate
			,	@CreatedByAuditId	= CreatedByAuditId	
			,	@ModifiedByAuditId	= ModifiedByAuditId
								
		FROM	dbo.Report
		WHERE	ReportId				= @ReportId

	EXEC dbo.ReportInsert 
			@ReportId			=	NULL
		,	@Name				=	@Name
		,	@Description		=	@Description
		,	@Title				=	@Title
		,	@CreatedDate		=	@CreatedDate
		,	@ModifiedDate		=	@ModifiedDate
		,	@CreatedByAuditId	=	@CreatedByAuditId
		,	@ModifiedByAuditId	=	@ModifiedByAuditId
		,	@SortOrder			=	@SortOrder
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'Report'
		,	@EntityKey				= @ReportId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
