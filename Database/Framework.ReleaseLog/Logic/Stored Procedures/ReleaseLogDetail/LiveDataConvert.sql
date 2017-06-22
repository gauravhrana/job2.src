IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDetailLiveDataConvert')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetailLiveDataConvert'
	DROP  Procedure  dbo.ReleaseLogDetailLiveDataConvert
END
GO

PRINT 'Creating Procedure ReleaseLogDetailLiveDataConvert'
GO

/******************************************************************************
**		File: 
**		Name: ReleaseLogList
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
**     ----------					   ---------
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

CREATE Procedure [dbo].[ReleaseLogDetailLiveDataConvert]
(
		@AuditId				INT		
	,	@ApplicationId			INT		
	,	@PrimaryDeveloper		VARCHAR(150)  	
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ReleaseLogDetail'
)
AS
BEGIN
	UPDATE ReleaseLogDetail
	SET ReleaseLogDetailId = ABS(ReleaseLogDetailId)
	where ReleaseLogDetailId < 0 AND PrimaryDeveloper = @PrimaryDeveloper

	PRINT 'ReleaseLogDetails test data has been converted to Live data successfully for ' + @PrimaryDeveloper

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO

