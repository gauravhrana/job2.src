IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ReleaseNotesModuleSearch')
BEGIN
	PRINT 'Dropping Procedure ReleaseNotesModuleSearch'
	DROP Procedure ReleaseNotesModuleSearch
END
GO

PRINT 'Creating Procedure ReleaseNotesModuleSearch'
GO

/******************************************************************************
**		File: 
**		Name: ReleaseNotesModuleSearch
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
			EXEC ReleaseNotesModuleSearch NULL	, NULL	, NULL
			EXEC ReleaseNotesModuleSearch NULL	, 'K'	, NULL
			EXEC ReleaseNotesModuleSearch 1		, 'K'	, NULL
			EXEC ReleaseNotesModuleSearch 1		, NULL	, NULL
			EXEC ReleaseNotesModuleSearch NULL	, NULL	, 'W'

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
ALTER procedure dbo.ReleaseNotesModuleSearch
(
	 
		@ReleaseLogId				INT				= NULL
	,	@ReleaseLogDetailId			INT				= NULL
	,	@ApplicationId				INT
	,	@AuditId					INT						
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'ReleaseLog'	 
)
AS
BEGIN

IF OBJECT_ID ('tempdb..#ReleaseLogSearch_Result') IS NULL

BEGIN	

CREATE table #ReleaseLogSearch_Result
	(
	ReleaseLogId       INT NOT NULL,
	ApplicationId      INT NOT NULL,
	ReleaseLogStatusId INT NOT NULL,
	Name               VARCHAR (50) NOT NULL,
	VersionNo          VARCHAR (50) NOT NULL,
	ReleaseDate        DATETIME NOT NULL,
	Description        VARCHAR (50) NOT NULL,
	SortOrder          INT NOT NULL,
	
	)

END

EXEC dbo.ReleaseLogSearch2 @ReleaseLogId = @ReleaseLogId,  @ApplicationId=@ApplicationId, @AuditId=@AuditId

IF OBJECT_ID ('tempdb..#ReleaseLogDetailSearch_Result') IS NULL
	
BEGIN

CREATE TABLE #ReleaseLogDetailSearch_Result
	(
	ReleaseLogDetailId       INT NOT NULL,
	ApplicationId            INT NOT NULL,
	ReleaseLogId             INT NOT NULL,
	ItemNo                   INT NOT NULL,
	Description              VARCHAR (50) NOT NULL,
	SortOrder                INT NOT NULL,
	RequestedBy              VARCHAR (50) NOT NULL,
	PrimaryDeveloper         VARCHAR (50) NOT NULL,
	RequestedDate            DATETIME NOT NULL,
	ReleaseIssueTypeId       INT NOT NULL,
	ReleasePublishCategoryId INT NOT NULL,	
	JIRA                     VARCHAR (50) NOT NULL,
	Feature                  VARCHAR (255) NOT NULL,
	PrimaryEntity            VARCHAR (225) NOT NULL,
	ReleaseLog       		 VARCHAR (150) NOT NULL,
	ReleaseIssueType       	 VARCHAR (150) NOT NULL,
	ReleasePublishCategory 	 VARCHAR (150) NOT NULL,
	Name 					 VARCHAR (150) NOT NULL,
	ReleaseLogStatus       	 VARCHAR (150) NOT NULL	
	)

	END	


EXEC ReleaseLogDetailSearch2 @ReleaseLogDetailId = @ReleaseLogDetailId, @ApplicationId=@ApplicationId, @AuditId=@AuditId

		SELECT	   a.ReleaseLogId              
		,	a.ApplicationId	
	    ,   b.ReleaseLogDetailId              
		,	b.ItemNo                    
		,	b.Description				
		,	b.SortOrder					
		,	b.RequestedBy               
		,	b.PrimaryDeveloper          
		,	b.RequestedDate
		,	b.ReleaseIssueTypeId			
		,	b.ReleasePublishCategoryId	
		,	b.JIRA						
		,	b.Feature					
		,	b.PrimaryEntity	
	   
	  
	FROM		#ReleaseLogSearch_Result	a 
		LEFT JOIN #ReleaseLogDetailSearch_Result b on a.ReleaseLogId=b.ReleaseLogId	
	 	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO

