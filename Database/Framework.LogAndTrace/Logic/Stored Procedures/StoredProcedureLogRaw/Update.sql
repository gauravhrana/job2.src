IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StoredProcedureLogRawUpdate')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogRawUpdate'
	DROP  Procedure  StoredProcedureLogRawUpdate
END
GO

PRINT 'Creating Procedure StoredProcedureLogRawUpdate'
GO

/******************************************************************************
**		File: 
**		Name: StoredProcedureLogRawUpdate
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

CREATE Procedure dbo.StoredProcedureLogRawUpdate
(
		@StoredProcedureLogRawId		INT			= NULL 			
	,	@StoredProcedureLogId			INT			= NULL 			
	,	@InputParameters				VARCHAR(500)					
	,	@InputValues					VARCHAR(5000)					
	,	@AuditId						INT							
	,	@AuditDate						DATETIME	= NULL			
	,	@SystemEntityType				VARCHAR(50)	= 'StoredProcedureLogRaw'
)
AS
BEGIN 

	UPDATE	dbo.StoredProcedureLogRaw 
	SET		StoredProcedureLogId		= @StoredProcedureLogId
		,	InputParameters				= @InputParameters
		,   InputValues					= @InputValues						
	WHERE	StoredProcedureLogRawId		= @StoredProcedureLogRawId

 END		
 GO