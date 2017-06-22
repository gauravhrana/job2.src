IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogChildrenGet')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogChildrenGet'
	DROP  Procedure ReleaseLogChildrenGet
END
GO

PRINT 'Creating Procedure ReleaseLogChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: ReleaseLogChildrenGet
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
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.ReleaseLogChildrenGet
(
		@ReleaseLogId			INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'ReleaseLog'
)
AS
BEGIN

	-- GET ReleaseLogDetail Records
	SELECT	a.ReleaseLogDetailId
		,	a.ApplicationId		
	    ,   a.ReleaseLogId              
		,	a.ItemNo                    
		,	a.Description				
		,	a.SortOrder					
		,	a.RequestedBy               
		,	a.PrimaryDeveloper          
		,	a.RequestedDate				
		,	b.Name AS 'ReleaseLog'
	FROM	dbo.ReleaseLogDetail a
	INNER JOIN ReleaseLog b ON a.ReleaseLogId = b.ReleaseLogId
	WHERE	a.ReleaseLogId = @ReleaseLogId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ReleaseLogId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   