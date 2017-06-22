IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AccidentReportDetails')
BEGIN
	PRINT 'Dropping Procedure AccidentReportDetails'
	DROP  Procedure AccidentReportDetails
END
GO

PRINT 'Creating Procedure AccidentReportDetails'
GO


/******************************************************************************
**		File: 
**		Name: AccidentReportDetails
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

CREATE Procedure dbo.AccidentReportDetails
(
		@AccidentReportID		INT
	,	@StudentID				INT				 = 0
	,	@Date					DATETIME		 = NULL
	,	@AccidentPlaceID		INT				 = 0
	,	@AccidentPlace			VARCHAR(50)		 = NULL
	,	@TeacherID				INT				 = 0
	,	@Description			VARCHAR(500)	 = NULL	
	,	@Remedy					VARCHAR(200)	 = NULL
	,	@SignoffParent			BIT				 
	,	@SignoffTeacher			BIT				 = NULL
	,	@SignoffAdmin			BIT				 = NULL
	,   @AuditId				INT			 
	,   @AuditDate				DATETIME		 = NULL
	,	@SystemEntityType		VARCHAR(50)		 = 'AccidentReport'	
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@AccidentReportId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	



	SELECT	AccidentReportId
		,	ApplicationId
	    ,	StudentId	
	    ,	Date
	    ,	AccidentPlaceId
	    ,	TeacherId 
		,	Description
		,	Remedy
		,	SignoffParent
		,	SignoffTeacher
		,	SignoffAdmin	
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'					
	FROM	dbo.AccidentReport 
	WHERE	AccidentReportId = @AccidentReportId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @AccidentReportId
		,	@AuditAction			= 'Details' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END	