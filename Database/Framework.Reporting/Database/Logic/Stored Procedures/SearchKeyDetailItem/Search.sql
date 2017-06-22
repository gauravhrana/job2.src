IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND Name='SearchKeyDetailItemSearch')
BEGIN
	PRINT 'Dropping Procedure SearchKeyDetailItemSearch'
	DROP Procedure SearchKeyDetailItemSearch
END
GO

PRINT 'Creating Procedure SearchKeyDetailItemSearch'
GO

/******************************************************************************
**		File: 
**		Activity: SearchKeyDetailItemSearch
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**
**		Sample:   
**              
			EXEC SearchKeyDetailItemSearch NULL	, NULL	, NULL
			EXEC SearchKeyDetailItemSearch NULL	, 'K'	, NULL
			EXEC SearchKeyDetailItemSearch 1	, 'K'	, NULL
			EXEC SearchKeyDetailItemSearch 1	, NULL	, NULL
			EXEC SearchKeyDetailItemSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
Create procedure SearchKeyDetailItemSearch
(
		@SearchKeyDetailItemId	    INT				= NULL 	
	,	@ApplicationId				INT				= NULL						
	,	@SearchKeyDetailId			INT				= NULL 						
	,	@AuditId					INT											
	,	@AuditDate					DATETIME		= NULL						
	,	@SystemEntityType			VARCHAR(50)		= 'SearchKeyDetailItem'		
	,	@ApplicationMode					INT				= NULL		
	,	@AddAuditInfo						INT				 = 1
	,	@AddTraceInfo						INT				 = 0
	,	@ReturnAuditInfo					INT				 = 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON	
	-- TRACE AND LOGGING ---
	DECLARE	@StoredProcedureLogId	INT
	DECLARE @InputParametersLocal	VARCHAR(50)  
	DECLARE @InputValuesLocal		VARCHAR(50)  
	SET @InputParametersLocal		= 'SearchKeyDetailId' 
	SET @InputValuesLocal			= CAST(@SearchKeyDetailId As VARCHAR(50)) 
	EXEC dbo.StoredProcedureLogInsert
			@StoredProcedureLogId		= @StoredProcedureLogId	OUTPUT
		,	@Name						= 'dbo.SearchKeyDetailItemSearch'	
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal
		,	@ExecutedBy					= @AuditId		
	SELECT	a.SearchKeyDetailItemId												
			,	a.SearchKeyDetailId	
			,   a.ApplicationId														
	        ,   b.SearchParameter    AS 'SearchKeyDetail'
			,	a.Value
			,	a.SortOrder	
	FROM 	dbo.SearchKeyDetailItem a
	INNER JOIN 	dbo.SearchKeyDetail	 b ON a.SearchKeyDetailId = b.SearchKeyDetailId 
	WHERE	a.SearchKeyDetailId	= ISNULL(@SearchKeyDetailId, a.SearchKeyDetailId)
	AND	a.SearchKeyDetailItemId	= ISNULL(@SearchKeyDetailItemId, a.SearchKeyDetailItemId)
	ORDER BY a.SearchKeyDetailId		ASC	,
			a.SearchKeyDetailItemId		ASC
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SearchKeyDetailItemId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId	    = @AuditId
	END

END
GO
	

