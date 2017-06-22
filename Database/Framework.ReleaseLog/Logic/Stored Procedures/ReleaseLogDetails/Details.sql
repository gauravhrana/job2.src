IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDetailDetails')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetailDetails'
	DROP  Procedure ReleaseLogDetailDetails
END
GO

PRINT 'Creating Procedure ReleaseLogDetailDetails'
GO


/******************************************************************************
**		File: 
**		Name: MilestoneDetails
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

CREATE Procedure dbo.ReleaseLogDetailDetails
(
		@ReleaseLogDetailId			INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME	= NULL	
	,	@SystemEntityType			VARCHAR(50)	= 'ReleaseLogDetails'
)
AS
BEGIN
	
	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@ReleaseLogDetailId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	a.ReleaseLogDetailId
		,	a.ApplicationId	
	    ,   a.ReleaseLogId              
		,	a.ItemNo                    
		,	a.Description				
		,	a.SortOrder					
		,	a.RequestedBy               
		,	a.PrimaryDeveloper          
		,	a.RequestedDate				
		,	b.Name				AS 'ReleaseLog'                    
		,	@LastUpdatedDate	AS	'UpdatedDate'
		,	@LastUpdatedBy		AS	'UpdatedBy'
		,	@LastAuditAction	AS	'LastAction'			
	FROM		 dbo.ReleaseLogDetail a
	INNER JOIN	 ReleaseLog			  b ON a.ReleaseLogId = b.ReleaseLogId
	WHERE		 ReleaseLogDetailId	  = @ReleaseLogDetailId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ReleaseLogDetailId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   