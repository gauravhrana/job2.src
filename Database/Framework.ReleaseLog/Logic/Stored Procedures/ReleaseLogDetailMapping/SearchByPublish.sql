ALTER procedure dbo.ReleaseLogDetailMappingSearchByPublishItem
(
		@ReleaseLogDetailMappingId			INT				= NULL 	
	,	@ParentReleaseLogDetailId			INT				= NULL 	
	,	@ChildReleaseLogDetailId			INT				= NULL 
	,	@ApplicationId						INT				= NULL 		
	,	@AuditId							INT							
	,	@AuditDate							DATETIME		= NULL	
	,	@SystemEntityType					VARCHAR(50)		= 'ReleaseLogDetailMapping'		
	,	@ApplicationMode					INT				= NULL		
	,	@AddAuditInfo						INT				= 1
	,	@AddTraceInfo						INT				= 0
	,	@ReturnAuditInfo					INT				= 0	 
)	
WITH RECOMPILE
AS
BEGIN
	
	SET	NOCOUNT ON

	SELECT	a.ReleaseLogDetailMappingId												
		,	a.ParentReleaseLogDetailId																
		,	a.ChildReleaseLogDetailId
		,	a.ApplicationId	
		,	b.ReleaseLogDetailId
		,	b.Description AS 'ChildReleaseLogDetail' FROM		dbo.ReleaseLogDetailMapping	a	   
			INNER JOIN ReleaseLogDetail b ON a.ChildReleaseLogDetailId=b.ReleaseLogDetailId
				
	
	WHERE a.ChildReleaseLogDetailId	= ISNULL(@ChildReleaseLogDetailId, a.ChildReleaseLogDetailId)
	AND a.ParentReleaseLogDetailId	= ISNULL(@ParentReleaseLogDetailId, a.ParentReleaseLogDetailId)
	AND a.ReleaseLogDetailMappingId			= ISNULL(@ReleaseLogDetailMappingId, a.ReleaseLogDetailMappingId)
	
	IF @AddAuditInfo = 1 
		BEGIN

			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= 'ReleaseLogDetailMapping'
				,	@EntityKey				= @ReleaseLogDetailMappingId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId
		
		END

END
GO

