IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SickReportDetails')
BEGIN
	PRINT 'Dropping Procedure SickReportDetails'
	DROP  Procedure SickReportDetails
END
GO

PRINT 'Creating Procedure SickReportDetails'
GO


/******************************************************************************
**		File: 
**		Name: SickReportDetails
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

CREATE Procedure dbo.SickReportDetails
(
		@SickReportId       INT			
	,	@AuditId			INT				
    ,   @AuditDate		    DATETIME	= NULL   
	,	@SystemEntityType	VARCHAR(50)	= 'SickReport' 
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@SickReportId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	SickReportId
		,	ApplicationId
		,   StudentId
		,	TypeOfSickness
		,	AmountOfSickness
		,	FreqOfSickness
		,   TeacherSickNote
		,   ReturnToSchoolDate
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'									
	FROM	SickReport 
	WHERE	SickReportId	= @SickReportId
	
--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @SickReportId
		,	@AuditAction			= 'Details' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END		
GO
   