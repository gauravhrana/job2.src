IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SickReportUpdate')
BEGIN
	PRINT 'Dropping Procedure SickReportUpdate'
	DROP  Procedure  SickReportUpdate
END
GO

PRINT 'Creating Procedure SickReportUpdate'

GO

/******************************************************************************
**		File: 
**		Name: SickReportUpdate
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
**		----------						-----------
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

CREATE Procedure dbo.SickReportUpdate
(       
		@SickReportId       INT
	,	@StudentId          INT           
	,	@TypeOfSickness     VARCHAR(50)   
	,	@AmountOfSickness   VARCHAR(50)   
	,	@FreqOfSickness     VARCHAR(50)   
	,	@TeacherSickNote    VARCHAR(100)  	
	,	@ReturnToSchoolDate DATETIME      
	,	@AuditId			INT			  
    ,   @AuditDate		    DATETIME		= NULL 
	,	@SystemEntityType	VARCHAR(50)		= 'SickReport' 
	 
)
AS
 BEGIN
	UPDATE	dbo.SickReport
	SET		SickReportId		=	@SickReportId	
		,	StudentId			=	@StudentId
		,	TypeOfSickness		=	@TypeOfSickness
		,	AmountOfSickness	=	@AmountOfSickness
		,	FreqOfSickness		=	@FreqOfSickness
		,	TeacherSickNote		=	@TeacherSickNote
		,	ReturnToSchoolDate	=	@ReturnToSchoolDate
	WHERE	SickReportId		=	@SickReportId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SickReportId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

