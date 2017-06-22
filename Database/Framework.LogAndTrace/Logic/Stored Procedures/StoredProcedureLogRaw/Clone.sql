IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogRawClone')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogRawClone'
	DROP  Procedure StoredProcedureLogRawClone
END
GO

PRINT 'Creating Procedure StoredProcedureLogRawClone'
GO

/*********************************************************************************************
**		File: 
**		Name: StoredProcedureLogRawClone
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
*********************************************************************************************
**		Change Histor
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.StoredProcedureLogRawClone
(
		@StoredProcedureLogRawId			INT			= NULL 				
	,	@StoredProcedureLogId				INT			= NULL 				
	,	@InputParameters					VARCHAR(500)						
	,	@InputValues						VARCHAR(5000)						
	,	@AuditId							INT									
	,	@AuditDate							DATETIME	= NULL				
	,	@SystemEntityType					VARCHAR(50)	= 'StoredProcedureLogRaw'
)
AS
BEGIN	
	
	SELECT	@StoredProcedureLogId		= StoredProcedureLogId
		,	@InputParameters			= InputParameters
		,   @InputValues				= InputValues			
	FROM	dbo.StoredProcedureLogRaw
	WHERE   StoredProcedureLogRawId		= @StoredProcedureLogRawId

	EXEC dbo.StoredProcedureLogRawInsert 
			@StoredProcedureLogRawId	=	NULL
		,	@StoredProcedureLogId		= @StoredProcedureLogId
		,	@InputParameters			= @InputParameters
		,   @InputValues				= @InputValues
	

END	
GO
